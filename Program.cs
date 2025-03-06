namespace IDAttendance
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //ATTENDANCE TRACKER USING STUDENTS' IDs


            Console.WriteLine("Welcome PUPian! \n");


            //for now, ids and name are stored in string
            string id1, id2, id3, name1, name2, name3;

            id1 = "2023-0001";
            id2 = "2024-0002";
            id3 = "2024-0003";
            name1 = "Yves";
            name2 = "JM";
            name3 = "Alfred";


            int inputProcess;


            //check if the inputted number is correct (1 or 2)
            do
            {


                //for listing the 2 process
                string[] process = { "1 - Clock-in", "2 - Clock-out" };
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
                if (!int.TryParse(Console.ReadLine(), out inputProcess) || (inputProcess != 1 && inputProcess != 2))
                {


                    Console.WriteLine("\nInvalid number.\n");


                }


            }


            //if input are not 1 and 2, the question will repeat, will not continue to next art
            while (inputProcess != 1 && inputProcess != 2);


            //for inputting the id
            string studentIdInput;


            /*
             * checking if the inputted id is correct
             * if true the question while not be repeated
             */
            bool checkIdInput = false;


            //check if the id inputted is equal to id1, id2, and id3
            do
            {


                //for entering id number
                Console.Write("\nEnter Your ID Number: ");
                studentIdInput = Console.ReadLine();


                /*
                 * choosing what proccess should be done/display
                 * case 1 for clock in
                 * case 2 for clock out
                 */
                switch (inputProcess)
                {


                    case 1:


                        //for checking each id and displaying name
                        if (studentIdInput == id1)
                        {


                            Console.WriteLine($"Welcome {name1}");
                            checkIdInput = true;


                        }


                        else if (studentIdInput == id2)
                        {


                            Console.WriteLine($"Welcome {name2}");
                            checkIdInput = true;


                        }


                        else if (studentIdInput == id3)
                        {


                            Console.WriteLine($"Welcome {name3}");
                            checkIdInput = true;


                        }


                        else
                        {


                            Console.WriteLine("Cannot find your ID on database, " +
                                "\nPlease check your inputted ID if correct");


                        }


                        break;


                    case 2:


                        if (studentIdInput == id1)
                        {


                            Console.WriteLine($"Goodbye {name1}");
                            checkIdInput = true;


                        }


                        else if (studentIdInput == id2)
                        {


                            Console.WriteLine($"Goodbye {name2}");
                            checkIdInput = true;


                        }


                        else if (studentIdInput == id3)
                        {


                            Console.WriteLine($"Goodbye {name3}");
                            checkIdInput = true;


                        }


                        else
                        {


                            Console.WriteLine("Cannot find your ID on database, " +
                                "\nPlease check your inputted ID if it is correct");


                        }


                        break;


                }


            }


            //if id is wrong, question will repeat and will not continue to switch statement
            while (!checkIdInput);


        }
    }
}
