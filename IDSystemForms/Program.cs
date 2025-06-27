using IDSystemBusinessLogic;
using IDSystemData.IDSystemData;
using WinFormsApp1;

namespace IDSystemForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //db, json, or txt
            const string modeOfStorage = "db";
            var studentStorageType = modeOfStorage == "db" ? "db" : modeOfStorage == "json" ? "DataStudents.json" : "StudentsAttendance.txt";
            var attendanceStorageType = modeOfStorage == "db" ? "db" : modeOfStorage == "json" ? "AttendanceStudents.json" : "StudentsAttendance.txt";


            //call type of storage to use
            var toStoreStudent = storageFormat.selectStudentStorage(modeOfStorage, studentStorageType);
            var toStoreAttendance = storageFormat.selectAttendanceStorage(modeOfStorage, attendanceStorageType);


            //start business logic
            Checking.setStorageLocation(toStoreStudent, toStoreAttendance);


            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new StudentForm());
        }
    }
}