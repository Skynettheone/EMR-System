namespace EMR
{
    public partial class loading : Form
    {
        public loading()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.Width += 3;
            if (panel2.Width >= 700)
            {
                timer1.Stop();
                Login f2 = new Login();
                f2.Show();
                this.Hide();
            }
        }


    }
}