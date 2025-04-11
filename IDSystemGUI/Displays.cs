using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDSystemBusinessLogic;

namespace IDSystemGUI
{
    public class Displays
    {

        //calling all method
        public static void DisplayStudentInfo()
        {
            

            Console.WriteLine($"\nWelcome, {Checking.studentName}!");
            Console.WriteLine(Checking.inOrout());
            Console.WriteLine(Checking.getSched());
            Console.WriteLine(Checking.getRecord());


        }


        public static string studentId
        {

            //only get ID and check
            get
            {


                string studentIdInput;
                bool checkId = false;


                do
                {


                    Console.Write("Enter Your ID Number: ");
                    studentIdInput = Console.ReadLine();



                    if (studentIdInput.ToLower() == "ex")
                        return studentIdInput;


                    if (Checking.checkId(studentIdInput))
                    {


                        checkId = true;


                    }


                    else
                    {


                        Console.WriteLine("ID not found. Please try again.\n");

                    }


                } 
                
                
                while (!checkId);


                return studentIdInput;


            }


        }

  
    }
}
