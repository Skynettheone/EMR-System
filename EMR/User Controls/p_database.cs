using System.Data;
using System.Data.SQLite;

namespace EMR.User_Controls
{
    public partial class p_database : UserControl
    {
        public p_database()
        {
            InitializeComponent();
        }

        private string selectedPatient_id;
        private void p_database_Load(object sender, EventArgs e)
        {
            GetUserData();
        }

        private void GetUserData()
        {
            using (var conn = new SQLiteConnection("Data Source=emr.db;Version=3;"))
            {
                string query = "SELECT * FROM Patient_General_data_Table";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                patientrecorddatagridview.DataSource = dt;
            }
        }

        private void patientrecorddatagridview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < patientrecorddatagridview.Rows.Count)
            {
                DataGridViewRow selectedRow = patientrecorddatagridview.Rows[e.RowIndex];
                name.Text = selectedRow.Cells[1].Value.ToString();
                addr.Text = selectedRow.Cells[8].Value.ToString();
                NIC.Text = selectedRow.Cells[3].Value.ToString();
                eadr.Text = selectedRow.Cells[7].Value.ToString();
                agefield.Text = selectedRow.Cells[4].Value.ToString();
                mobilenum.Text = selectedRow.Cells[5].Value.ToString();
                telenum.Text = selectedRow.Cells[6].Value.ToString();
                selectedPatient_id = selectedRow.Cells[0].Value.ToString();
                updatebtn.Enabled = true;
            }
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (patientrecorddatagridview.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a row to update.");
                    return;
                }
                DataGridViewRow selectedRow = patientrecorddatagridview.SelectedRows[0];
                string patientId = selectedRow.Cells[0].Value.ToString();
                using (var conn = new SQLiteConnection("Data Source=emr.db;Version=3;"))
                {
                    conn.Open();
                    string updateQuery = "UPDATE Patient_General_data_Table SET Full_Name = @Full_Name, DOB = @DOB, NIC = @NIC, Age = @Age, Mobile_Number = @Mobile_Number, Telephone_Number = @Telephone_Number, Email = @Email, Patient_Address = @Patient_Address WHERE Patient_id = @Patient_id";
                    using (var cmd = new SQLiteCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Full_Name", name.Text);
                        cmd.Parameters.AddWithValue("@DOB", DOBpicker.Value.Date.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@Age", CalculateAge(DOBpicker.Value));
                        cmd.Parameters.AddWithValue("@NIC", NIC.Text);
                        cmd.Parameters.AddWithValue("@Patient_Address", addr.Text);
                        cmd.Parameters.AddWithValue("@Mobile_Number", mobilenum.Text);
                        cmd.Parameters.AddWithValue("@Telephone_Number", telenum.Text);
                        cmd.Parameters.AddWithValue("@Email", eadr.Text);
                        cmd.Parameters.AddWithValue("@Patient_id", patientId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data updated successfully!");
                            GetUserData();
                        }
                        else
                        {
                            MessageBox.Show("Data update failed.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
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

        private void searchbtn_Click(object sender, EventArgs e)
        {
            string searchTerm = searchfield.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("Please enter a search term.");
                return;
            }
            foreach (DataGridViewRow row in patientrecorddatagridview.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }
            bool found = false;
            foreach (DataGridViewRow row in patientrecorddatagridview.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    {
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                        found = true;
                        break;
                    }
                }
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == searchTerm)
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                    found = true;
                    break;
                }
                if (found)
                {
                    break;
                }
            }
            if (!found)
            {
                MessageBox.Show("Search result not found.");
            }
            searchfield.Text = "";
        }
    }
}
