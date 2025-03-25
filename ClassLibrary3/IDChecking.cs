using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3
{
    public class IDChecking
    {
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


                if (checkIDIfValid(studentIdInput))
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


        //check if the id inputted is equal to id1, id2, and id3
        static bool checkIDIfValid(string studentIdInput)
        {


            string[] validIds = { "2023-0001", "2024-0002", "2024-0003" };


            return validIds.Contains(studentIdInput);


        }
    }
}
