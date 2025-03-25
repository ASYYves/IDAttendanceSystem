using System;
using ClassLibrary2;
using ClassLibrary3;
namespace IDAttendance
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //ATTENDANCE TRACKER USING STUDENTS' IDs


            Console.WriteLine("Welcome PUPian! \n");


            int inputProcess = DisplayProcesses.getInput();


            string studentIdInput = IDChecking.getStudentID(inputProcess);


            processAttendance(inputProcess, studentIdInput);


        }


        static void processAttendance(int inputProcess, string studentIdInput)
        {


            string id1 = "2023-0001", id2 = "2024-0002", id3 = "2024-0003";
            string name1 = "Yves", name2 = "JM", name3 = "Alfred";


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
                        

                    }


                    else if (studentIdInput == id2)
                    {


                        Console.WriteLine($"Welcome {name2}");
                        

                    }


                    else if (studentIdInput == id3)
                    {


                        Console.WriteLine($"Welcome {name3}");                        


                    }

                    
                    break;


                case 2:


                    if (studentIdInput == id1)
                    {


                        Console.WriteLine($"Goodbye {name1}");                        


                    }


                    else if (studentIdInput == id2)
                    {


                        Console.WriteLine($"Goodbye {name2}");                        


                    }


                    else if (studentIdInput == id3)
                    {


                        Console.WriteLine($"Goodbye {name3}");                       


                    }


                    break;


                    case 3:

                    //temporary stored in string and same set of schedule on all ID
                    if (studentIdInput == id1 || studentIdInput == id2 || studentIdInput == id3) {


                        Console.WriteLine("\n--- Class Schedule ---");
                        Console.WriteLine("Monday - Friday: 8:00 AM - 5:00 PM");
                        Console.WriteLine("Saturday: 9:00 AM - 2:00 PM");
                        Console.WriteLine("Sunday: No Classes\n");


                    }

                    break;


                    case 4:

                    //provide random number for lates and absents
                    Random random = new Random();
                    int randomNumber1 = random.Next(1, 10);
                    int randomNumber2 = random.Next(1, 10);


                    if (studentIdInput == id1)
                    {


                        Console.WriteLine($"Number of Lates: {randomNumber1}");
                        Console.WriteLine($"Number of Absents: {randomNumber2}");


                    }


                    else if (studentIdInput == id2)
                    {


                        Console.WriteLine($"Number of Lates: {randomNumber1}");
                        Console.WriteLine($"Number of Absents: {randomNumber2}");


                    }


                    else if (studentIdInput == id3)
                    {


                        Console.WriteLine($"Number of Lates: {randomNumber1}");
                        Console.WriteLine($"Number of Absents: {randomNumber2}");


                    }


                    break;

            }
        }


    }
}
