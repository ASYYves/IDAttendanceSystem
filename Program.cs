using IDSystemBusinessLogic;
using IDSystemData;
using IDSystemData.IDSystemData;
using IDSystemGUI;
using System;
using System.Threading;
using Microsoft.Extensions.Configuration; 
using System.IO; 

namespace IDAttendance
{

    class Program
    {


        static void Main(string[] args)
        {


            
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();

            
            var smtpSettings = config.GetSection("SmtpSettings").Get<Smtp>();

            
            Email.Initialize(smtpSettings);
            


            //db, json, or txt
            const string modeOfStorage = "db";
            var studentStorageType = modeOfStorage == "db" ? "db" : modeOfStorage == "json" ? "DataStudents.json" : "StudentsAttendance.txt";
            var attendanceStorageType = modeOfStorage == "db" ? "db" : modeOfStorage == "json" ? "AttendanceStudents.json" : "StudentsAttendance.txt";


            //call type of storage to use
            var toStoreStudent = storageFormat.selectStudentStorage(modeOfStorage, studentStorageType);
            var toStoreAttendance = storageFormat.selectAttendanceStorage(modeOfStorage, attendanceStorageType);


            //start business logic
            Checking.setStorageLocation(toStoreStudent, toStoreAttendance);


            //loop until exit
            while (true)
            {


                Console.WriteLine("Welcome PUPIan!\n");
                Checking.AutoClockOutAll(TimeSpan.FromMinutes(10));


                //return ex, admin or valid ID
                var id = Displays.StudentId;
                if (id.Equals("ex", StringComparison.OrdinalIgnoreCase))
                {


                    Console.WriteLine("Loop ends");
                    break;


                }


                //show student info
                Displays.displayStudentInfo();
                Console.WriteLine("\n\n\n");


            }


        }



    }


}
