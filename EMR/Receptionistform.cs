using EMR.User_Controls;

namespace EMR
{
    public partial class Receptionistform : Form
    {
        public Receptionistform()
        {
            InitializeComponent();
            p_register uc = new p_register();
            addUserContorls(uc);
        }

        private void addUserContorls(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void pregisterbtn_Click(object sender, EventArgs e)
        {
            p_register register = new p_register();
            addUserContorls(register);
        }

        private void pdatabasebtn_Click(object sender, EventArgs e)
        {
            p_database databsep = new p_database();
            addUserContorls(databsep);

        }

        private void pdateassignbtn_Click(object sender, EventArgs e)
        {
            p_dateassign dateassign = new p_dateassign();
            addUserContorls(dateassign);
        }

        private void pappointmentbtn_Click(object sender, EventArgs e)
        {
            p_appointment appointment = new p_appointment();
            addUserContorls(appointment);
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

        private void Receptionistform_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timelable.Text = DateTime.Now.ToShortTimeString();
            datelabel.Text = DateTime.Now.ToLongDateString();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timelable.Text = DateTime.Now.ToShortTimeString();
            timer1.Start();
        }
    }
}
