using EMR.User_Controls;

namespace EMR
{
    public partial class systemadmin : Form
    {
        public systemadmin()
        {
            InitializeComponent();
            U_Register uc = new U_Register();
            addUserContorls(uc);

        }

        private void addUserContorls(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }



        private void databasebtn_Click(object sender, EventArgs e)
        {
            database databse = new database();
            addUserContorls(databse);
        }

        private void registerbtn_Click(object sender, EventArgs e)
        {
            U_Register register = new U_Register();
            addUserContorls(register);
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            DialogResult res;
            res = MessageBox.Show("Do you want to logout", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                Login logoutForm = new Login();
                logoutForm.Show();
                this.Hide();
            }
            else
            {
                this.Show();
            }
        }

        private void systemadmin_Load(object sender, EventArgs e)
        {

        }
    }
}
