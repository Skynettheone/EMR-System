using System.Data;
using System.Data.SQLite;


namespace EMR
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        // SQLite connection string (database file in application directory)
        SQLiteConnection conn = new SQLiteConnection("Data Source=emr.db;Version=3;");


        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void loginbutton_Click(object sender, EventArgs e)
        {
            string username, password;
            username = usernamefield.Text;
            password = passwordfield.Text;


            try
            {
                string query = "SELECT * FROM E_Register WHERE User_id = @Username AND Password = @Password";
                SQLiteDataAdapter sda = new SQLiteDataAdapter(query, conn);

                sda.SelectCommand.Parameters.AddWithValue("@Username", usernamefield.Text);
                sda.SelectCommand.Parameters.AddWithValue("@Password", passwordfield.Text);

                DataTable dtable = new DataTable();
                sda.Fill(dtable);

                if (dtable.Rows.Count > 0)
                {

                    string userRole = dtable.Rows[0]["Role"].ToString();


                    switch (userRole)
                    {
                        case "Admin":
                            // Open AdminForm
                            systemadmin menuform = new systemadmin();
                            menuform.Show();
                            break;
                        case "Doctor":
                            // Open UserForm
                            Doctorform doctorform = new Doctorform();
                            doctorform.Show();
                            break;

                        case "Receptionist":
                            // Open UserForm
                            Receptionistform recepform = new Receptionistform();
                            recepform.Show();
                            break;

                        case "Lab Director":
                            // Open UserForm
                            Lab_directorform directorform = new Lab_directorform();
                            directorform.Show();
                            break;

                        default:
                            MessageBox.Show("Invalid user role", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid login details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    usernamefield.Clear();
                    passwordfield.Clear();
                    usernamefield.Focus();
                }
            }
            catch
            {
                MessageBox.Show("An error occurred");
            }
            finally
            {
                conn.Close();
            }
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            usernamefield.Clear();
            passwordfield.Clear();


            usernamefield.Focus();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Do you want to exit", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                this.Show();
            }
        }
    }
}
