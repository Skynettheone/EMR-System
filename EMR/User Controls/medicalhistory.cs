using System.Data;
using System.Data.SQLite;

namespace EMR.User_Controls
{
    public partial class medicalhistory : UserControl
    {
        private DataTable originalDataTable;
        public medicalhistory()
        {
            InitializeComponent();
            LoadDoctorData();
        }

        private void LoadDoctorData()
        {
            try
            {
                using (var connection = new SQLiteConnection("Data Source=emr.db;Version=3;"))
                {
                    connection.Open();
                    string query = "SELECT * FROM Doctor_pInfo_Table";
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                    originalDataTable = new DataTable();
                    adapter.Fill(originalDataTable);
                    doctorDataGridView.DataSource = originalDataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading data: " + ex.Message);
            }
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            string searchTerm = searchfield.Text.Trim();
            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Please enter a search term.");
                return;
            }
            DataTable searchResults = originalDataTable.Clone();
            foreach (DataRow row in originalDataTable.Rows)
            {
                string patientID = row["Patient_id"].ToString();
                string fullName = row["Full_Name"].ToString();
                if (patientID.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || fullName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                {
                    searchResults.ImportRow(row);
                }
            }
            doctorDataGridView.DataSource = searchResults;
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            doctorDataGridView.DataSource = originalDataTable;
            searchfield.Clear();
        }
    }
}
