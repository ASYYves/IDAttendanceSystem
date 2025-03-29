using System;
using IDSystemGUI;
using IDSystemBusinessLogic;

namespace IDAttendance
{

    internal class Program
    {

        static void Main(string[] args)
        {
            //ATTENDANCE TRACKER USING STUDENTS' IDs


            Console.WriteLine("Welcome PUPian! \n");


            int inputProcess = Displays.getInput();


            string studentIdInput = Displays.getStudentID(inputProcess);


            Checking.processAttendance(inputProcess, studentIdInput);


            string displayName = Checking.processAttendance(inputProcess, studentIdInput);
            Console.WriteLine(displayName);


        }


    }
}
