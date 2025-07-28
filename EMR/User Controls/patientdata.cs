using System.Data.SQLite;
using System.Data;
using System.Drawing;
using System.IO;

namespace EMR.User_Controls
{
    public partial class patientdata : UserControl
    {
        public patientdata()
        {
            InitializeComponent();
            AutoCompleteStringCollection idAutoComplete = new AutoCompleteStringCollection();
            idAutoComplete.AddRange(GetPatientIDsFromDatabase());
            patientidfield.AutoCompleteCustomSource = idAutoComplete;
            patientidfield.AutoCompleteMode = AutoCompleteMode.Suggest;
            patientidfield.AutoCompleteSource = AutoCompleteSource.CustomSource;
            MedCon_BOX.KeyDown += RichTextBox_KeyDown;
            PreBOX.KeyDown += RichTextBox_KeyDown;
            AllergiesBOX.KeyDown += RichTextBox_KeyDown;
            Special_ConBTN.KeyDown += RichTextBox_KeyDown;
        }

        private string[] GetPatientIDsFromDatabase()
        {
            using (var connection = new SQLiteConnection("Data Source=emr.db;Version=3;"))
            {
                connection.Open();
                string query = "SELECT Patient_id FROM Patient_General_data_Table";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        var patientIDs = new System.Collections.Generic.List<string>();
                        while (reader.Read())
                        {
                            patientIDs.Add(reader["Patient_id"].ToString());
                        }
                        return patientIDs.ToArray();
                    }
                }
            }
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            string selectedPatientID = patientidfield.Text;
            if (string.IsNullOrEmpty(selectedPatientID))
            {
                MessageBox.Show("Please enter a patient ID.");
                return;
            }
            try
            {
                using (var connection = new SQLiteConnection("Data Source=emr.db;Version=3;"))
                {
                    connection.Open();
                    string patientDataQuery = "SELECT Full_Name, Age FROM Patient_General_data_Table WHERE Patient_id = @Patient_id";
                    using (var patientDataCommand = new SQLiteCommand(patientDataQuery, connection))
                    {
                        patientDataCommand.Parameters.AddWithValue("@Patient_id", selectedPatientID);
                        var patientDataReader = patientDataCommand.ExecuteReader();
                        if (patientDataReader.Read())
                        {
                            name.Text = patientDataReader["Full_Name"].ToString();
                            agefield.Text = patientDataReader["Age"].ToString();
                            // Gender column not present in schema, so skip
                        }
                        else
                        {
                            MessageBox.Show("Patient not found.");
                            return;
                        }
                        patientDataReader.Close();
                    }
                    string labReportQuery = "SELECT Report_id, Report_Date, Report_Type, Result FROM Lab_Report_Table WHERE Patient_id = @Patient_id";
                    using (var labReportCommand = new SQLiteCommand(labReportQuery, connection))
                    {
                        labReportCommand.Parameters.AddWithValue("@Patient_id", selectedPatientID);
                        var labReportReader = labReportCommand.ExecuteReader();
                        appointmentidfield.Text = string.Empty;
                        testtedon.Text = string.Empty;
                        testedforfield.Text = string.Empty;
                        imageFilesListBox.Items.Clear();
                        while (labReportReader.Read())
                        {
                            appointmentidfield.Text += labReportReader["Report_id"].ToString() + ", ";
                            testtedon.Text += labReportReader["Report_Date"].ToString() + ", ";
                            testedforfield.Text += labReportReader["Report_Type"].ToString() + ", ";
                            // No image file in schema, so skip
                        }
                        labReportReader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void RichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            RichTextBox richTextBox = (RichTextBox)sender;
            if (e.KeyCode == Keys.Enter && e.Shift)
            {
                e.SuppressKeyPress = true;
                int bulletIndex = richTextBox.SelectionStart;
                string bulletPoint = "\u2022 ";
                richTextBox.SelectedText = "\n" + bulletPoint;
                richTextBox.SelectionStart = bulletIndex + bulletPoint.Length + 1;
            }
            else if (richTextBox.Lines.Length == 0 || richTextBox.Lines[0].Length == 0)
            {
                int bulletIndex = richTextBox.SelectionStart;
                string bulletPoint = "\u2022 ";
                richTextBox.SelectedText = bulletPoint;
                richTextBox.SelectionStart = bulletIndex + bulletPoint.Length;
            }
        }

        private void savbtn_Click(object sender, EventArgs e)
        {
            string selectedPatientID = patientidfield.Text;
            string fullname = name.Text;
            string medCon = MedCon_BOX.Text;
            string preCon = PreBOX.Text;
            string allergies = AllergiesBOX.Text;
            string specialCon = Special_ConBTN.Text;
            string appointmentIDs = appointmentidfield.Text;
            string testedOnDates = testtedon.Text;
            string testedForDetails = testedforfield.Text;
            string age = agefield.Text;
            string gender = gendefield.Text;
            if (string.IsNullOrEmpty(selectedPatientID))
            {
                MessageBox.Show("Please enter a patient ID.");
                return;
            }
            try
            {
                using (var connection = new SQLiteConnection("Data Source=emr.db;Version=3;"))
                {
                    connection.Open();
                    string insertQuery = @"
                        INSERT INTO Doctor_pInfo_Table (Patient_id, Full_Name, Age, Gender, AppointmentID, Tested_on, Tested_for, Medical_Condition, Prescriptions, Allergies, Special_Conditions)
                        VALUES (@Patient_id, @Full_Name, @Age, @Gender, @AppointmentID, @Tested_on, @Tested_for, @MedCon, @PreCon, @Allergies, @SpecialCon)
                    ";
                    using (var insertCommand = new SQLiteCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@Patient_id", selectedPatientID);
                        insertCommand.Parameters.AddWithValue("@Full_Name", fullname);
                        insertCommand.Parameters.AddWithValue("@Age", age);
                        insertCommand.Parameters.AddWithValue("@Gender", gender);
                        insertCommand.Parameters.AddWithValue("@AppointmentID", appointmentIDs);
                        insertCommand.Parameters.AddWithValue("@Tested_on", testedOnDates);
                        insertCommand.Parameters.AddWithValue("@Tested_for", testedForDetails);
                        insertCommand.Parameters.AddWithValue("@MedCon", medCon);
                        insertCommand.Parameters.AddWithValue("@PreCon", preCon);
                        insertCommand.Parameters.AddWithValue("@Allergies", allergies);
                        insertCommand.Parameters.AddWithValue("@SpecialCon", specialCon);
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data saved successfully.");
                            patientidfield.Clear();
                            name.Clear();
                            agefield.Clear();
                            gendefield.Clear();
                            appointmentidfield.Clear();
                            testtedon.Clear();
                            testedforfield.Clear();
                            MedCon_BOX.Clear();
                            PreBOX.Clear();
                            AllergiesBOX.Clear();
                            Special_ConBTN.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Data could not be saved.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
