using System;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace EMR
{
    public static class DatabaseInitializer
    {
        public static void InitializeDatabase()
        {
            string dbPath = "emr.db";
            bool dbExists = File.Exists(dbPath);

            if (!dbExists)
            {
                SQLiteConnection.CreateFile(dbPath);
                MessageBox.Show("Database created!");
            }

            using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                conn.Open();

                // Create E_Register table
                string createERegister = @"
                CREATE TABLE IF NOT EXISTS E_Register (
                    User_id TEXT PRIMARY KEY,
                    Name TEXT,
                    User_Address TEXT,
                    NIC TEXT,
                    Email TEXT,
                    Mobile TEXT,
                    Password TEXT,
                    Role TEXT
                );";
                new SQLiteCommand(createERegister, conn).ExecuteNonQuery();

                // Create Patient_General_data_Table
                string createPatientTable = @"
                CREATE TABLE IF NOT EXISTS Patient_General_data_Table (
                    Patient_id TEXT PRIMARY KEY,
                    Full_Name TEXT,
                    DOB TEXT,
                    NIC TEXT,
                    Age INTEGER,
                    Gender TEXT,
                    Mobile_Number TEXT,
                    Telephone_Number TEXT,
                    Email TEXT,
                    Patient_Address TEXT
                );";
                new SQLiteCommand(createPatientTable, conn).ExecuteNonQuery();

                // Create Appointment_Table with FK to Patient_General_data_Table
                string createAppointmentTable = @"
                CREATE TABLE IF NOT EXISTS Appointment_Table (
                    AppointmentID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Patient_id TEXT,
                    Full_Name TEXT,
                    NIC TEXT,
                    AppointmentDate TEXT,
                    AppointmentTime TEXT,
                    Mobile_Number TEXT,
                    Email TEXT,
                    FOREIGN KEY(Patient_id) REFERENCES Patient_General_data_Table(Patient_id)
                );";
                new SQLiteCommand(createAppointmentTable, conn).ExecuteNonQuery();

                // Create Lab_Report_Table with FK to Patient_General_data_Table and Appointment_Table
                string createLabReportTable = @"
                CREATE TABLE IF NOT EXISTS Lab_Report_Table (
                    Report_id INTEGER PRIMARY KEY AUTOINCREMENT,
                    AppointmentID INTEGER,
                    Patient_id TEXT,
                    Full_Name TEXT,
                    Tested_on TEXT,
                    Tested_for TEXT,
                    Tested_lab TEXT,
                    Tested_by TEXT,
                    Test_report_file_name TEXT,
                    Test_Report_File BLOB,
                    Report_Date TEXT,
                    Report_Type TEXT,
                    Result TEXT,
                    FOREIGN KEY(Patient_id) REFERENCES Patient_General_data_Table(Patient_id),
                    FOREIGN KEY(AppointmentID) REFERENCES Appointment_Table(AppointmentID)
                );";
                new SQLiteCommand(createLabReportTable, conn).ExecuteNonQuery();

                // Create Doctor_pInfo_Table with FK to Patient_General_data_Table
                string createDoctorInfoTable = @"
                CREATE TABLE IF NOT EXISTS Doctor_pInfo_Table (
                    Info_id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Patient_id TEXT,
                    Full_Name TEXT,
                    Age INTEGER,
                    Gender TEXT,
                    AppointmentID TEXT,
                    Tested_on TEXT,
                    Tested_for TEXT,
                    Medical_Condition TEXT,
                    Prescriptions TEXT,
                    Allergies TEXT,
                    Special_Conditions TEXT,
                    FOREIGN KEY(Patient_id) REFERENCES Patient_General_data_Table(Patient_id)
                );";
                new SQLiteCommand(createDoctorInfoTable, conn).ExecuteNonQuery();

                // MIGRATION: Add Gender column if missing
                string checkGenderColumn = "PRAGMA table_info(Patient_General_data_Table);";
                bool genderExists = false;
                using (var cmd = new SQLiteCommand(checkGenderColumn, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader[1].ToString() == "Gender")
                        {
                            genderExists = true;
                            break;
                        }
                    }
                }
                if (!genderExists)
                {
                    string alterTable = "ALTER TABLE Patient_General_data_Table ADD COLUMN Gender TEXT;";
                    new SQLiteCommand(alterTable, conn).ExecuteNonQuery();
                }

                // MIGRATION: Ensure Gender column is TEXT
                bool genderIsText = false;
                string checkGenderType = "PRAGMA table_info(Patient_General_data_Table);";
                using (var cmd = new SQLiteCommand(checkGenderType, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader[1].ToString() == "Gender" && reader[2].ToString().ToUpper() == "TEXT")
                        {
                            genderIsText = true;
                            break;
                        }
                    }
                }
                if (!genderIsText)
                {
                    // Migrate table: create new table, copy data, drop old, rename new
                    string createTempTable = @"
                        CREATE TABLE IF NOT EXISTS Patient_General_data_Table_temp (
                            Patient_id TEXT PRIMARY KEY,
                            Full_Name TEXT,
                            DOB TEXT,
                            NIC TEXT,
                            Age INTEGER,
                            Gender TEXT,
                            Mobile_Number TEXT,
                            Telephone_Number TEXT,
                            Email TEXT,
                            Patient_Address TEXT
                        );";
                    new SQLiteCommand(createTempTable, conn).ExecuteNonQuery();
                    string copyData = @"
                        INSERT INTO Patient_General_data_Table_temp (Patient_id, Full_Name, DOB, NIC, Age, Gender, Mobile_Number, Telephone_Number, Email, Patient_Address)
                        SELECT Patient_id, Full_Name, DOB, NIC, Age, CAST(Gender AS TEXT), Mobile_Number, Telephone_Number, Email, Patient_Address FROM Patient_General_data_Table;";
                    new SQLiteCommand(copyData, conn).ExecuteNonQuery();
                    new SQLiteCommand("DROP TABLE Patient_General_data_Table;", conn).ExecuteNonQuery();
                    new SQLiteCommand("ALTER TABLE Patient_General_data_Table_temp RENAME TO Patient_General_data_Table;", conn).ExecuteNonQuery();
                }

                // Only insert dummy data if E_Register is empty
                string checkERegister = "SELECT COUNT(*) FROM E_Register;";
                var cmdCheck = new SQLiteCommand(checkERegister, conn);
                long userCount = (long)cmdCheck.ExecuteScalar();
                if (userCount == 0)
                {
                    string insertERegister = @"
                    INSERT INTO E_Register (User_id, Name, User_Address, NIC, Email, Mobile, Password, Role) VALUES
                    ('admin1', 'Alice Admin', '123 Admin St', '900000000V', 'alice@admin.com', '0711111111', 'adminpass', 'Admin'),
                    ('doc1', 'Bob Doctor', '456 Doctor Rd', '900000001V', 'bob@doctor.com', '0722222222', 'docpass', 'Doctor'),
                    ('recep1', 'Carol Receptionist', '789 Reception Ave', '900000002V', 'carol@recep.com', '0733333333', 'receppass', 'Receptionist'),
                    ('lab1', 'Dave Lab', '101 Lab Blvd', '900000003V', 'dave@lab.com', '0744444444', 'labpass', 'Lab Director');";
                    new SQLiteCommand(insertERegister, conn).ExecuteNonQuery();
                }

                // Only insert dummy data if Patient_General_data_Table is empty
                string checkPatientTable = "SELECT COUNT(*) FROM Patient_General_data_Table;";
                var cmdCheckPatient = new SQLiteCommand(checkPatientTable, conn);
                long patientCount = (long)cmdCheckPatient.ExecuteScalar();
                if (patientCount == 0)
                {
                    string insertPatientTable = @"
                    INSERT INTO Patient_General_data_Table (Patient_id, Full_Name, DOB, NIC, Age, Gender, Mobile_Number, Telephone_Number, Email, Patient_Address) VALUES
                    ('P_01', 'Eve Patient', '1980-01-01', '800000000V', 44, 'Female', '0755555555', '0111234567', 'eve@patient.com', '12 Patient St'),
                    ('P_02', 'Frank Patient', '1990-05-15', '900000010V', 34, 'Male', '0766666666', '0117654321', 'frank@patient.com', '34 Patient Rd');";
                    new SQLiteCommand(insertPatientTable, conn).ExecuteNonQuery();
                }

                // Only insert dummy data if Lab_Report_Table is empty
                string checkLabReportTable = "SELECT COUNT(*) FROM Lab_Report_Table;";
                var cmdCheckLab = new SQLiteCommand(checkLabReportTable, conn);
                long labCount = (long)cmdCheckLab.ExecuteScalar();
                if (labCount == 0)
                {
                    string insertLabReportTable = @"
                    INSERT INTO Lab_Report_Table (Patient_id, Report_Date, Report_Type, Result) VALUES
                    ('P_01', '2024-06-01', 'Blood Test', 'Normal'),
                    ('P_02', '2024-06-02', 'Urine Test', 'Normal');";
                    new SQLiteCommand(insertLabReportTable, conn).ExecuteNonQuery();
                }

                // Only insert dummy data if Doctor_pInfo_Table is empty
                string checkDoctorInfoTable = "SELECT COUNT(*) FROM Doctor_pInfo_Table;";
                var cmdCheckDoctor = new SQLiteCommand(checkDoctorInfoTable, conn);
                long doctorInfoCount = (long)cmdCheckDoctor.ExecuteScalar();
                if (doctorInfoCount == 0)
                {
                    string insertDoctorInfoTable = @"
                    INSERT INTO Doctor_pInfo_Table (Patient_id, Full_Name, Age, Gender, AppointmentID, Tested_on, Tested_for, Medical_Condition, Prescriptions, Allergies, Special_Conditions) VALUES
                    ('P_01', 'Eve Patient', 44, 'Female', 'A100', '2024-06-01', 'Blood Test', 'Healthy', 'Paracetamol', 'None', 'None'),
                    ('P_02', 'Frank Patient', 34, 'Male', 'A101', '2024-06-02', 'Urine Test', 'Diabetic', 'Metformin', 'Penicillin', 'Diet Control');";
                    new SQLiteCommand(insertDoctorInfoTable, conn).ExecuteNonQuery();
                }

                // Only insert dummy data if Appointment_Table is empty
                string checkAppointmentTable = "SELECT COUNT(*) FROM Appointment_Table;";
                var cmdCheckAppointment = new SQLiteCommand(checkAppointmentTable, conn);
                long appointmentCount = (long)cmdCheckAppointment.ExecuteScalar();
                if (appointmentCount == 0)
                {
                    string insertAppointmentTable = @"
                    INSERT INTO Appointment_Table (Patient_id, Full_Name, NIC, AppointmentDate, AppointmentTime, Mobile_Number, Email) VALUES
                    ('P_01', 'Eve Patient', '800000000V', '2024-06-10', '09:00', '0755555555', 'eve@patient.com'),
                    ('P_02', 'Frank Patient', '900000010V', '2024-06-11', '10:30', '0766666666', 'frank@patient.com');";
                    new SQLiteCommand(insertAppointmentTable, conn).ExecuteNonQuery();
                }

                conn.Close();
            }
        }
    }
}
