using EMR.User_Controls;

namespace EMR
{
    public partial class Lab_directorform : Form
    {
        public Lab_directorform()
        {
            InitializeComponent();
            p_appointment uc = new p_appointment();
            addUserContorls(uc);
        }

        private void addUserContorls(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void uploadreportbtn_Click(object sender, EventArgs e)
        {
            Uploadreport uploadreport = new Uploadreport();
            addUserContorls(uploadreport);
        }

        private void appointmentbtn_Click(object sender, EventArgs e)
        {
            p_appointment appointment = new p_appointment();
            addUserContorls(appointment);
        }

        private void databasebtn_Click(object sender, EventArgs e)
        {
            lab_database labdatabse = new lab_database();
            addUserContorls(labdatabse);
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
    }
}
