using System;
using IDSystemGUI;
using IDSystemBusinessLogic;
using IDSystemData;

namespace IDAttendance
{

    internal class Program
    {

        static void Main(string[] args)
        {


            //ATTENDANCE TRACKER USING STUDENTS' IDs


            //run on txt file
            string studentFilePath = "StudentsData.txt";
            Data.loadDataOfStudents(studentFilePath);
            string attendancePath = "StudentsAttendance.txt";
            Checking.setAttendanceOfStudents(attendancePath);


            //run on json file
            //string studentFilePath = "DataStudents.json";
            //dataOfStudents.LoadStudentData(studentFilePath);
            //string attendancePath = "AttendanceStudents.json";
            //Checking.setAttendanceOfStudents(attendancePath);


            //string studentFilePath = "db";
            //Data.loadDataOfStudents(studentFilePath);
            //string attendancePath = "db";
            //Checking.setAttendanceOfStudents(attendancePath);


            //loop the whole program
            while (true)
            {


                Console.WriteLine("Welcome PUPian! \n");


                string studentIdInput = Displays.studentId;


                
                //for ending the loop
                if (studentIdInput.ToLower() == "ex")
                {


                    Console.WriteLine("LOOP ENDS");
                    break;


                }


                //for getting student info
                Checking.studentId = studentIdInput;


                //displaying info
                Displays.DisplayStudentInfo();


                //giving space for next input
                Console.WriteLine("\n\n\n\n");
                
               
                
            }

            

        }


    }


}
