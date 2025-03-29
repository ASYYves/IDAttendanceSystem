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


        public static int getInput()
        {


            int inputProcess;


            //check if the inputted number is correct (1 or 2)
            do
            {


                //for listing the 2 process
                string[] process = { "1 - Clock-in", "2 - Clock-out", "3 - Check Schedule", "4 - View Attendance" };
                Console.WriteLine("Are you clocking in or out?");


                foreach (string display in process)
                {


                    Console.WriteLine(display);


                }


                //for inputting what process should be done
                Console.Write("\nEnter Number: ");


                /*
                 * for checking if the number entered are 1 or 2, 
                 * if not, will reask the question and display invalid number
                 */
                if (!int.TryParse(Console.ReadLine(), out inputProcess) || (inputProcess < 1 || inputProcess > 4))
                {


                    Console.WriteLine("\nInvalid number.\n");


                }


            }


            //if input are not 1 and 2, the question will repeat, will not continue to next art
            while (inputProcess < 1 || inputProcess > 4);


            return inputProcess;


        }


        public static string getStudentID(int inputProcess)
        {


            //for inputting the id
            string studentIdInput;


            /*
             * checking if the inputted id is correct
             * if true the question while not be repeated
             */
            bool checkIdInput = false;


            do
            {


                //for entering id number
                Console.Write("\nEnter Your ID Number: ");
                studentIdInput = Console.ReadLine();


                if (Checking.checkIDIfValid(studentIdInput))
                {

                    // if ID is valid, continue to next step
                    checkIdInput = true;


                }


                else
                {


                    Console.WriteLine("Cannot find your ID in the database, \nPlease check your inputted ID and try again.");


                }


            }


            //if id is wrong, question will repeat and will not continue to switch statement
            while (!checkIdInput);


            return studentIdInput;


        }


    }
}
