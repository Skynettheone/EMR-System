using System.Data;
using System.Data.SQLite;

namespace EMR.User_Controls
{
    public partial class lab_database : UserControl
    {
        private DataGridViewImageColumn imageColumn;
        public lab_database()
        {
            InitializeComponent();
            ConfigureDataGridView();
            GetUserData();
            labdatabasegridview.CellClick += labdatabasegridview_CellClick_1;
        }
        private void ConfigureDataGridView()
        {
            labdatabasegridview.AutoGenerateColumns = true;
            labdatabasegridview.AllowUserToAddRows = false;
            labdatabasegridview.AllowUserToDeleteRows = false;
            labdatabasegridview.ReadOnly = true;
            labdatabasegridview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void GetUserData()
        {
            try
            {
                using (var conn = new SQLiteConnection("Data Source=emr.db;Version=3;"))
                {
                    string query = "SELECT * FROM Lab_Report_Table";
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    labdatabasegridview.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
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
            foreach (DataGridViewRow row in labdatabasegridview.Rows)
            {
                bool rowMatch = false;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    {
                        rowMatch = true;
                        break;
                    }
                }
                if (rowMatch)
                {
                    row.DefaultCellStyle.BackColor = Color.Yellow;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }
        private void OpenImageForm(byte[] imageData)
        {
            using (ImageViewerForm imageViewer = new ImageViewerForm(imageData))
            {
                imageViewer.ShowDialog();
            }
        }
        private void labdatabasegridview_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = labdatabasegridview.Rows[e.RowIndex];
                string appointmentId = row.Cells[1].Value?.ToString();
                string patientId = row.Cells[2].Value?.ToString();
                string fullName = row.Cells[3].Value?.ToString();
                string testedOn = row.Cells[4].Value?.ToString();
                string testedFor = row.Cells[5].Value?.ToString();
                string testedLab = row.Cells[6].Value?.ToString();
                string testedBy = row.Cells[7].Value?.ToString();
                string reportFileName = row.Cells[8].Value?.ToString();
                object reportFileObj = row.Cells[9].Value;
                // Show details in a MessageBox (or set to controls if you have them)
                string details = $"Appointment ID: {appointmentId}\nPatient ID: {patientId}\nFull Name: {fullName}\nTested On: {testedOn}\nTested For: {testedFor}\nTested Lab: {testedLab}\nTested By: {testedBy}\nReport File Name: {reportFileName}";
                MessageBox.Show(details, "Report Details");
                // Show image if present
                if (reportFileObj != null && reportFileObj is byte[] imageData && imageData.Length > 0)
                {
                    OpenImageForm(imageData);
                }
            }
        }
    }
}
