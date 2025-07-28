using System.Data.SqlClient;

namespace EMR.User_Controls
{
    public partial class U_Register : UserControl
    {
        public U_Register()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DELL-INSPIRON\SQLEXPRESS;Initial Catalog=""EMR System"";Integrated Security=True");
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Save_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(name.Text) || string.IsNullOrWhiteSpace(addr.Text) || string.IsNullOrWhiteSpace(NIC.Text))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            try
            {
                conn.Open();


                string sql = "INSERT INTO E_Register (Name, User_Address, NIC, Email, Mobile, User_id, Password, Role) " +
                             "VALUES (@Name, @Residential_Address, @NIC, @Email_Address, @Mobile_Number, @User_ID, @Password, @Role)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", name.Text);
                    cmd.Parameters.AddWithValue("@Residential_Address", addr.Text);
                    cmd.Parameters.AddWithValue("@NIC", NIC.Text);
                    cmd.Parameters.AddWithValue("@Email_Address", eadr.Text);
                    cmd.Parameters.AddWithValue("@Mobile_Number", tp.Text);
                    cmd.Parameters.AddWithValue("@User_ID", uid.Text);
                    cmd.Parameters.AddWithValue("@Password", pw.Text);
                    cmd.Parameters.AddWithValue("@Role", roleselect.SelectedItem.ToString());

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data inserted successfully.");

                        name.Clear();
                        addr.Clear();
                        NIC.Clear();
                        eadr.Clear();
                        tp.Clear();
                        uid.Clear();
                        pw.Clear();

                    }
                    else
                    {
                        MessageBox.Show("Data insertion failed.");
                        name.Clear();
                        addr.Clear();
                        NIC.Clear();
                        eadr.Clear();
                        tp.Clear();
                        uid.Clear();
                        pw.Clear();

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
