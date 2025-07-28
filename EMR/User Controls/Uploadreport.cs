using System.Data.SQLite;

namespace EMR.User_Controls
{
    public partial class Uploadreport : UserControl
    {
        private List<Report> reports = new List<Report>();
        private bool filesSelected = false;
        public Uploadreport()
        {
            InitializeComponent();
            InitializeAutoComplete();
            InitializeEventHandlers();
        }

        private void InitializeAutoComplete()
        {
            AutoCompleteStringCollection idAutoComplete = new AutoCompleteStringCollection();
            idAutoComplete.AddRange(GetappointmentIDsFromDatabase());
            appointmentidfield.AutoCompleteCustomSource = idAutoComplete;
            appointmentidfield.AutoCompleteMode = AutoCompleteMode.Suggest;
            appointmentidfield.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void InitializeEventHandlers()
        {
            searchbtnappointment.Click += searchbtnappointment_Click;
            addreportbtn.Click += addreportbtn_Click;
            savereportsbtn.Click += savereportsbtn_Click;
        }

        private string[] GetappointmentIDsFromDatabase()
        {
            using (var connection = new SQLiteConnection("Data Source=emr.db;Version=3;"))
            {
                connection.Open();
                string query = "SELECT AppointmentID FROM Appointment_Table";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        var appointmentIDs = new System.Collections.Generic.List<string>();
                        while (reader.Read())
                        {
                            appointmentIDs.Add(reader["AppointmentID"].ToString());
                        }
                        return appointmentIDs.ToArray();
                    }
                }
            }
        }

        private void searchbtnappointment_Click(object sender, EventArgs e)
        {
            string selectedappointmentID = appointmentidfield.Text;
            if (string.IsNullOrEmpty(selectedappointmentID))
            {
                MessageBox.Show("Please enter an Appointment ID.");
                return;
            }
            try
            {
                using (var connection = new SQLiteConnection("Data Source=emr.db;Version=3;"))
                {
                    connection.Open();
                    string query = "SELECT Full_Name, Patient_id FROM Appointment_Table WHERE AppointmentID = @AppointmentID";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AppointmentID", selectedappointmentID);
                        var reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            patientidfield.Text = reader["Patient_id"].ToString();
                            name.Text = reader["Full_Name"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Appointment not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void savereportsbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(appointmentidfield.Text) ||
                string.IsNullOrWhiteSpace(patientidfield.Text) ||
                string.IsNullOrWhiteSpace(name.Text) ||
                string.IsNullOrWhiteSpace(testedforfield.Text) ||
                string.IsNullOrWhiteSpace(testedlabfield.Text) ||
                string.IsNullOrWhiteSpace(testedbyfield.Text) ||
                !filesSelected)
            {
                MessageBox.Show("Please fill in all the required fields and select files before saving.");
                return;
            }
            string selectedappointmentID = appointmentidfield.Text;
            DateTime selectedDate = testedondatepicker.Value;
            try
            {
                using (var connection = new SQLiteConnection("Data Source=emr.db;Version=3;"))
                {
                    connection.Open();
                    foreach (var report in reports)
                    {
                        string insertQuery = "INSERT INTO Lab_Report_Table (AppointmentID, Patient_id, Full_Name, Tested_on, Tested_for, Tested_lab, Tested_by, Test_report_file_name, Test_Report_File) " +
                                            "VALUES (@AppointmentID, @Patient_id, @Full_Name, @Tested_on, @Tested_for, @Tested_lab, @Tested_by, @Test_report_file_name, @Test_Report_File)";
                        using (var command = new SQLiteCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@AppointmentID", selectedappointmentID);
                            command.Parameters.AddWithValue("@Patient_id", patientidfield.Text);
                            command.Parameters.AddWithValue("@Full_Name", name.Text);
                            command.Parameters.AddWithValue("@Tested_on", selectedDate.ToString("yyyy-MM-dd"));
                            command.Parameters.AddWithValue("@Tested_for", testedforfield.Text);
                            command.Parameters.AddWithValue("@Tested_lab", testedlabfield.Text);
                            command.Parameters.AddWithValue("@Tested_by", testedbyfield.Text);
                            command.Parameters.AddWithValue("@Test_report_file_name", report.FileName);
                            command.Parameters.AddWithValue("@Test_Report_File", report.ReportImage);
                            command.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Reports saved successfully.");
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void addreportbtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif|All Files (*.*)|*.*";
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filesSelected = true;
                    string imagePath = openFileDialog.FileName;
                    Report newReport = new Report
                    {
                        FileName = Path.GetFileName(imagePath),
                        ReportImage = File.ReadAllBytes(imagePath),
                    };
                    reports.Clear();
                    reports.Add(newReport);
                    imageFilesListBox.Items.Clear();
                    imageFilesListBox.Items.Add(newReport.FileName);
                }
            }
        }

        public class Report
        {
            public string FileName { get; set; }
            public byte[] ReportImage { get; set; }
        }

        private void ClearFields()
        {
            appointmentidfield.Clear();
            patientidfield.Clear();
            name.Clear();
            testedforfield.Clear();
            testedlabfield.Clear();
            testedbyfield.Clear();
            imageFilesListBox.Items.Clear();
            reports.Clear();
            filesSelected = false;
        }
    }
}
