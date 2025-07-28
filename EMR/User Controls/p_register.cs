using System.Data.SQLite;
using System.Data;

namespace EMR.User_Controls
{
    public partial class p_register : UserControl
    {
        public p_register()
        {
            InitializeComponent();
            Save.Click += Save_Click;
            DOBpicker.ValueChanged += DOBpicker_ValueChanged;
            RetrieveAndDisplayData();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(name.Text) || DOBpicker.Value == DateTimePicker.MinimumDateTime || (!gendermale.Checked && !genderfemale.Checked))
            {
                MessageBox.Show("Please fill all the required fields.");
                return;
            }
            try
            {
                using (var connection = new SQLiteConnection("Data Source=emr.db;Version=3;"))
                {
                    connection.Open();
                    string nextPatientId = GenerateNextPatientId(connection);
                    string query = "INSERT INTO Patient_General_data_Table (Patient_id, Full_Name, DOB, Age, Gender, NIC, Patient_Address, Mobile_Number, Telephone_Number, Email) " +
                                   "VALUES (@Patient_id, @Full_Name, @DOB, @Age, @Gender, @NIC, @Patient_Address, @Mobile_Number, @Telephone_Number, @Email)";
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@Patient_id", nextPatientId);
                    command.Parameters.AddWithValue("@Full_Name", name.Text);
                    command.Parameters.AddWithValue("@DOB", DOBpicker.Value.Date.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@Age", CalculateAge(DOBpicker.Value));
                    command.Parameters.AddWithValue("@Gender", gendermale.Checked ? "Male" : "Female");
                    command.Parameters.AddWithValue("@NIC", NIC.Text);
                    command.Parameters.AddWithValue("@Patient_Address", addr.Text);
                    command.Parameters.AddWithValue("@Mobile_Number", mobilenum.Text);
                    command.Parameters.AddWithValue("@Telephone_Number", telenum.Text);
                    command.Parameters.AddWithValue("@Email", eadr.Text);
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Patient data successfully saved. Patient ID: {nextPatientId}");
                        ClearFields();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private string GenerateNextPatientId(SQLiteConnection connection)
        {
            string query = "SELECT Patient_id FROM Patient_General_data_Table ORDER BY Patient_id DESC LIMIT 1";
            using (var command = new SQLiteCommand(query, connection))
            {
                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    string lastId = result.ToString();
                    if (lastId.StartsWith("P_"))
                    {
                        int num = int.Parse(lastId.Substring(2));
                        return $"P_{(num + 1).ToString("D2")}";
                    }
                }
                return "P_01";
            }
        }

        private void DOBpicker_ValueChanged(object sender, EventArgs e)
        {
            agefield.Text = CalculateAge(DOBpicker.Value).ToString();
        }

        private int CalculateAge(DateTime birthDate)
        {
            int age = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now.DayOfYear < birthDate.DayOfYear)
            {
                age--;
            }
            return age;
        }

        private void ClearFields()
        {
            name.Clear();
            DOBpicker.Value = DateTimePicker.MinimumDateTime;
            agefield.Clear();
            gendermale.Checked = false;
            genderfemale.Checked = false;
            NIC.Clear();
            addr.Clear();
            mobilenum.Clear();
            telenum.Clear();
            eadr.Clear();
        }

        private void RetrieveAndDisplayData()
        {
            // You can implement this to show patient data in a grid if needed, using SQLite
        }
    }
}
