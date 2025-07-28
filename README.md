# EMR System (.NET 6 MVP)

> **Note:** This project is a basic MVP (Minimum Viable Product) for an Electronic Medical Records (EMR) system. It implements essential features to demonstrate core functionality and is intended as a foundation for further development and refinement.


## Project Overview
This repository contains the source code for an Electronic Medical Records (EMR) system developed as a Minimum Viable Product (MVP) using .NET 6 (Windows Forms). The EMR system is designed to streamline and digitize medical record management for healthcare facilities, supporting multiple user roles and core clinical workflows.

### Key Features
- **Patient Registration & Management:** Add, update, and view patient details and medical history.
- **Appointment Scheduling:** Manage patient appointments and doctor assignments.
- **Lab Report Management:** Upload, view, and manage laboratory reports.
- **Role-Based Access:** Includes modules for doctors, lab directors, receptionists, and system administrators.
- **User Authentication:** Secure login and access control for different user types.


## Database Transition
The project was initially built with a fully relational database using SQL Server Management Studio (SSMS). Due to technical issues and data loss, the backend was migrated to SQLite to restore functionality. While this change enabled continued development, some database features may be limited or require further refinement. Future updates will address these limitations and restore advanced relational features.


## Current Status
- Core MVP features are implemented:
  - Patient registration and management
  - Appointment scheduling
  - Lab report uploads and management
  - Doctor, lab director, receptionist, and admin modules
- Data storage is handled via SQLite.
- Some features may be incomplete or contain bugs due to the database migration.



## File Structure
The main directories and files in this project:

```
EMR.sln
README.md
EMR/
├── DatabaseInitializer.cs
├── Doctorform.cs
├── Doctorform.Designer.cs
├── Doctorform.resx
├── EMR.csproj
├── EMR.csproj.user
├── ImageViewerForm.cs
├── ImageViewerForm.Designer.cs
├── ImageViewerForm.resx
├── Lab_directorform.cs
├── Lab_directorform.Designer.cs
├── Lab_directorform.resx
├── loading.cs
├── loading.Designer.cs
├── loading.resx
├── Login.cs
├── Login.Designer.cs
├── Login.resx
├── Program.cs
├── Receptionistform.cs
├── Receptionistform.Designer.cs
├── Receptionistform.resx
├── systemadmin.cs
├── systemadmin.Designer.cs
├── systemadmin.resx
├── bin/
│   └── Debug/
│       └── net6.0-windows/
│           ├── emr.db
│           ├── EMR.exe
│           ├── EMR.dll
│           ├── ...
├── obj/
│   └── Debug/
│       └── net6.0-windows/
│           ├── apphost.exe
│           ├── ...
├── Properties/
│   ├── Resources.Designer.cs
│   ├── Resources.resx
├── Resources/
│   ├── Login.png
│   ├── PngItem_1503945.png
│   ├── pngwing.com (9).png
├── User Controls/
│   ├── database.cs
│   ├── database.Designer.cs
│   ├── database.resx
│   ├── lab_database.cs
│   ├── lab_database.Designer.cs
│   ├── lab_database.resx
│   ├── medicalhistory.cs
│   ├── medicalhistory.Designer.cs
│   ├── medicalhistory.resx
│   ├── p_appointment.cs
│   ├── p_appointment.Designer.cs
│   ├── p_appointment.resx
│   ├── p_database.cs
│   ├── p_database.Designer.cs
│   ├── p_database.resx
│   ├── p_dateassign.cs
│   ├── p_dateassign.Designer.cs
│   ├── p_dateassign.resx
│   ├── p_register.cs
│   ├── p_register.Designer.cs
│   ├── p_register.resx
│   ├── patientdata.cs
│   ├── patientdata.Designer.cs
│   ├── patientdata.resx
│   ├── U_Register.cs
│   ├── U_Register.Designer.cs
│   ├── U_Register.resx
│   ├── Uploadreport.cs
│   ├── Uploadreport.Designer.cs
│   ├── Uploadreport.resx
```

## How to Run Locally
To run this EMR MVP project on your PC:

1. **Requirements:**
   - Windows OS
   - [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) installed
   - Visual Studio 2022 or later (recommended for Windows Forms projects)

2. **Clone the Repository:**
   - Download or clone the project to your local machine.

3. **Open the Solution:**
   - Open `EMR.sln` in Visual Studio.

4. **Restore NuGet Packages:**
   - Visual Studio will automatically restore required packages on project load. If not, right-click the solution and select `Restore NuGet Packages`.

5. **Build the Project:**
   - Click `Build > Build Solution` or press `Ctrl+Shift+B`.

6. **Run the Application:**
   - Press `F5` or click `Start` to run the application.

7. **Database:**
   - The SQLite database file (`emr.db`) is created automatically in the `bin/Debug/net6.0-windows/` directory on first run.


## Future Development
- Refactor the database and codebase to follow industry standards and best practices
- Expand core features and add new modules (e.g., prescription management, reporting)
- Improve code quality, architecture, and documentation
- Restore and enhance relational integrity and advanced features as originally planned with SSMS

If you encounter issues, ensure all dependencies are installed and your environment matches the requirements above.


## Contributing
Contributions, suggestions, and issue reports are welcome as the project evolves. Please open an issue or submit a pull request for improvements.


---
*This project is under active development. Expect frequent updates and improvements.*
