using System.Data;
using System.Data.SQLite;

namespace EMR.User_Controls
{
    public partial class p_appointment : UserControl
    {
        private BindingSource bindingSource = new BindingSource();
        private SQLiteConnection conn = new SQLiteConnection("Data Source=emr.db;Version=3;");

        public p_appointment()
        {
            InitializeComponent();
        }

        private void p_appointment_Load(object sender, EventArgs e)
        {
            GetUserData();
            DateTime today = DateTime.Today;
            int appointmentCount = GetAppointmentCountForDate(today);
            appointmentcount.Text = $"Appointments for {today:MM/dd/yyyy}: {appointmentCount} appointments";
        }

        private void GetUserData()
        {
            string query = "SELECT * FROM Appointment_Table";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            bindingSource.DataSource = dt;
            appointmentrecorddatagridview.DataSource = bindingSource;
            Clearbtn.Click += Clearbtn_Click;
        }

        private int GetAppointmentCountForDate(DateTime date)
        {
            int appointmentCount = 0;
            try
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Appointment_Table WHERE AppointmentDate = @SelectedDate";
                using (var command = new SQLiteCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@SelectedDate", date.ToString("yyyy-MM-dd"));
                    appointmentCount = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            finally
            {
                conn.Close();
            }
            return appointmentCount;
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = datePickersearch.Value.Date;
            if (bindingSource.DataSource is DataTable dt)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = $"AppointmentDate = '{selectedDate:yyyy-MM-dd}'";
                appointmentrecorddatagridview.DataSource = dv;
            }
        }

        private void Clearbtn_Click(object sender, EventArgs e)
        {
            ClearSearchResults();
        }

        private void ClearSearchResults()
        {
            if (bindingSource.DataSource is DataTable dt)
            {
                dt.DefaultView.RowFilter = string.Empty;
                appointmentrecorddatagridview.DataSource = bindingSource;
            }
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            if (appointmentrecorddatagridview.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment to update.");
                return;
            }
            DataGridViewRow selectedRow = appointmentrecorddatagridview.SelectedRows[0];
            string appointmentId = selectedRow.Cells[0].Value.ToString();
            try
            {
                conn.Open();
                string updateQuery = "UPDATE Appointment_Table SET AppointmentDate = @AppointmentDate, AppointmentTime = @AppointmentTime WHERE AppointmentID = @AppointmentID";
                using (var cmd = new SQLiteCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@AppointmentDate", selectedRow.Cells[4].Value.ToString());
                    cmd.Parameters.AddWithValue("@AppointmentTime", selectedRow.Cells[5].Value.ToString());
                    cmd.Parameters.AddWithValue("@AppointmentID", appointmentId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Appointment updated successfully!");
                        GetUserData();
                    }
                    else
                    {
                        MessageBox.Show("Appointment update failed.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (appointmentrecorddatagridview.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment to delete.");
                return;
            }
            DataGridViewRow selectedRow = appointmentrecorddatagridview.SelectedRows[0];
            string appointmentId = selectedRow.Cells[0].Value.ToString();
            try
            {
                conn.Open();
                string deleteQuery = "DELETE FROM Appointment_Table WHERE AppointmentID = @AppointmentID";
                using (var cmd = new SQLiteCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@AppointmentID", appointmentId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Appointment deleted successfully!");
                        GetUserData();
                    }
                    else
                    {
                        MessageBox.Show("Appointment deletion failed.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
