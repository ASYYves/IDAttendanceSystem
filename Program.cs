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


            while (true)
            {


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
                Console.WriteLine("\n\n");
                
               
                
            }

            

        }


    }
}
