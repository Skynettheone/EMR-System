using System.Data;
using System.Data.SQLite;

namespace EMR.User_Controls
{
    public partial class database : UserControl
    {
        public database()
        {
            InitializeComponent();
        }

        // SQLite connection string
        SQLiteConnection conn = new SQLiteConnection("Data Source=emr.db;Version=3;");
        public int User_id;
        private void database_Load(object sender, EventArgs e)
        {
            GetUserData();
        }

        private void GetUserData()
        {
            string query = "SELECT * FROM E_Register";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            userrecorddatagridview.DataSource = dt;
        }

        private void userrecorddatagridview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < userrecorddatagridview.Rows.Count)
            {
                DataGridViewRow selectedRow = userrecorddatagridview.Rows[e.RowIndex];
                name.Text = selectedRow.Cells[1].Value.ToString(); // Name
                addr.Text = selectedRow.Cells[2].Value.ToString(); // User_Address
                NIC.Text = selectedRow.Cells[3].Value.ToString(); // NIC
                eadr.Text = selectedRow.Cells[4].Value.ToString(); // Email
                tp.Text = selectedRow.Cells[5].Value.ToString(); // Mobile
                uid.Text = selectedRow.Cells[0].Value.ToString(); // User_id
                pw.Text = selectedRow.Cells[6].Value.ToString(); // Password
                updatebtn.Enabled = true;
            }
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            try
            {
                string updateQuery = "UPDATE E_Register SET Name = @Name, User_Address = @Residential_Address, NIC = @NIC, Email = @Email_Address, Mobile = @Mobile_Number, Password = @Password WHERE User_id = @User_ID";
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=emr.db;Version=3;"))
                using (SQLiteCommand cmd = new SQLiteCommand(updateQuery, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Name", name.Text);
                    cmd.Parameters.AddWithValue("@Residential_Address", addr.Text);
                    cmd.Parameters.AddWithValue("@NIC", NIC.Text);
                    cmd.Parameters.AddWithValue("@Email_Address", eadr.Text);
                    cmd.Parameters.AddWithValue("@Mobile_Number", tp.Text);
                    cmd.Parameters.AddWithValue("@User_ID", uid.Text);
                    cmd.Parameters.AddWithValue("@Password", pw.Text);
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
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            string searchTerm = searchfield.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("Please enter a search term.");
                return;
            }
            foreach (DataGridViewRow row in userrecorddatagridview.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }
            bool found = false;
            foreach (DataGridViewRow row in userrecorddatagridview.Rows)
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
            }
            if (!found)
            {
                MessageBox.Show("Search result not found.");
            }
            searchfield.Text = "";
        }
    }
}
