using EMR.User_Controls;

namespace EMR
{
    public partial class Doctorform : Form
    {
        public Doctorform()
        {
            InitializeComponent();
            patientdata uc = new patientdata();
            addUserContorls(uc);
        }

        private void addUserContorls(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void patientdatabtn_Click(object sender, EventArgs e)
        {
            patientdata patientData = new patientdata();
            addUserContorls(patientData);
        }

        private void medicalhistorybtn_Click(object sender, EventArgs e)
        {
            medicalhistory medicalhistoryu = new medicalhistory();
            addUserContorls(medicalhistoryu);
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
