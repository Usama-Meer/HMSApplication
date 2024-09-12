using System;
using System.Data.SqlClient;

namespace HMSApplication.Data
{
    public class DatabaseConnection
    {
        private string connectionString = "Server=(localdb)\\mssqllocaldb;Database=hmsDB;Trusted_Connection=True;MultipleActiveResultSets=true";

        // SQL Scripts
        private string createUserTable = @"
            CREATE TABLE IF NOT EXISTS Users (
                UserId INT PRIMARY KEY IDENTITY,
                Username NVARCHAR(50) NOT NULL,
                Password NVARCHAR(50) NOT NULL,
                RoleId INT NOT NULL
            )";

        private string createRoleTable = @"
            CREATE TABLE IF NOT EXISTS Roles (
                RoleId INT PRIMARY KEY IDENTITY,
                RoleName NVARCHAR(50) NOT NULL
            )";

        private string createDoctorTable = @"
            CREATE TABLE IF NOT EXISTS Doctors (
                DoctorId INT PRIMARY KEY IDENTITY,
                FirstName NVARCHAR(50) NOT NULL,
                LastName NVARCHAR(50) NOT NULL,
                Speciality NVARCHAR(50),
                Fee DECIMAL(18, 2),
                Phone NVARCHAR(15),
                Email NVARCHAR(50)
            )";

        private string createPatientTable = @"
            CREATE TABLE IF NOT EXISTS Patients (
                PatientId INT PRIMARY KEY IDENTITY,
                FirstName NVARCHAR(50) NOT NULL,
                LastName NVARCHAR(50) NOT NULL,
                Dob DATE,
                Gender NVARCHAR(10),
                Phone NVARCHAR(15),
                Email NVARCHAR(50),
                Address NVARCHAR(100)
            )";

        private string createAppointmentTable = @"
            CREATE TABLE IF NOT EXISTS Appointments (
                AppointmentId INT PRIMARY KEY IDENTITY,
                PatientId INT,
                DoctorId INT,
                Status NVARCHAR(50),
                AppointmentDate DATETIME,
                FOREIGN KEY (PatientId) REFERENCES Patients(PatientId),
                FOREIGN KEY (DoctorId) REFERENCES Doctors(DoctorId)
            )";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public void InitializeDatabase()
        {
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();

                    // Execute table creation scripts
                    ExecuteSqlScript(connection, createUserTable);
                    ExecuteSqlScript(connection, createRoleTable);
                    ExecuteSqlScript(connection, createDoctorTable);
                    ExecuteSqlScript(connection, createPatientTable);
                    ExecuteSqlScript(connection, createAppointmentTable);

                    Console.WriteLine("Database tables created successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Database initialization failed: {ex.Message}");
                }
            }
        }

        private void ExecuteSqlScript(SqlConnection connection, string script)
        {
            using (SqlCommand command = new SqlCommand(script, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
