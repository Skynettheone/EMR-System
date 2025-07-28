using System.Data.SQLite;
using System.Data;

namespace EMR.User_Controls
{
    public partial class p_dateassign : UserControl
    {
        public p_dateassign()
        {
            InitializeComponent();
            AutoCompleteStringCollection idAutoComplete = new AutoCompleteStringCollection();
            idAutoComplete.AddRange(GetPatientIDsFromDatabase());
            patientidfield.AutoCompleteCustomSource = idAutoComplete;
            patientidfield.AutoCompleteMode = AutoCompleteMode.Suggest;
            patientidfield.AutoCompleteSource = AutoCompleteSource.CustomSource;
            Timepicker.Format = DateTimePickerFormat.Time;
            Timepicker.ShowUpDown = true;
            searchbtn.Click += searchbtn_Click;
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
                    string query = "SELECT Full_Name, Mobile_Number, Email, NIC FROM Patient_General_data_Table WHERE Patient_id = @Patient_id";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Patient_id", selectedPatientID);
                        var reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            name.Text = reader["Full_Name"].ToString();
                            mobilenum.Text = reader["Mobile_Number"].ToString();
                            eadr.Text = reader["Email"].ToString();
                            NIC.Text = reader["NIC"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Patient not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void appointment_Click(object sender, EventArgs e)
        {
            string selectedPatientID = patientidfield.Text;
            DateTime selectedDate = DatePicker.Value;
            TimeSpan selectedTime = Timepicker.Value.TimeOfDay;
            DateTime combinedDateTime = selectedDate.Add(selectedTime);
            string formattedTime = combinedDateTime.ToString("HH:mm");
            try
            {
                using (var connection = new SQLiteConnection("Data Source=emr.db;Version=3;"))
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO Appointment_Table (Patient_id, Full_Name, NIC, AppointmentDate, AppointmentTime, Mobile_Number, Email) " +
                                        "VALUES (@Patient_id, @Full_Name, @NIC, @AppointmentDate, @AppointmentTime, @Mobile_Number, @Email);";
                    using (var command = new SQLiteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Patient_id", selectedPatientID);
                        command.Parameters.AddWithValue("@Full_Name", name.Text);
                        command.Parameters.AddWithValue("@NIC", NIC.Text);
                        command.Parameters.AddWithValue("@AppointmentDate", selectedDate.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@AppointmentTime", formattedTime);
                        command.Parameters.AddWithValue("@Mobile_Number", mobilenum.Text);
                        command.Parameters.AddWithValue("@Email", eadr.Text);
                        command.ExecuteNonQuery();
                        long newAppointmentID = connection.LastInsertRowId;
                        MessageBox.Show($"Appointment created successfully. Appointment ID: {newAppointmentID}");
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
