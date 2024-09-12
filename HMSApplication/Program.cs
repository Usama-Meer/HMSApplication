using HMSApplication.Data;

namespace HMSApplication
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            //initializing the database
            DatabaseConnection db = new DatabaseConnection();
            db.InitializeDatabase();



            Application.Run(new Form1());
        }
    }
}