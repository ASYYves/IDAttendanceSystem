using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDSystemBusinessLogic
{
    public class Checking
    {
        


        //check if the id inputted is equal to id1, id2, and id3
        public static bool checkIDIfValid(string studentIdInput)
        {


            return validIds.ContainsKey(studentIdInput);


        }


        private static readonly Dictionary<string, string> validIds = new Dictionary<string, string>
        {


            { "2023-0001", "Yves" },
            { "2024-0002", "JM" },
            { "2024-0003", "Alfred" }


        };


        private static readonly Dictionary<string, string> attendances = new Dictionary<string, string>
        {


            { "2023-0001", "\n--- Schedule for Yves ---\n" +
                "Monday - Friday: 8:00 AM - 12:00 PM\n" +
                "Saturday: 10:00 AM - 1:00 PM\n" +
                "Sunday: No Classes\n" },


            { "2024-0002", "\n--- Schedule for JM ---\n" +
                "Monday - Friday: 1:00 PM - 5:00 PM\n" +
                "Saturday: 9:00 AM - 12:00 PM\n" +
                "Sunday: No Classes\n" },


            { "2024-0003", "\n--- Schedule for Alfred ---\n" +
                "Monday - Friday: 9:00 AM - 3:00 PM\n" +
                "Saturday: 8:00 AM - 11:00 AM\n" +
                "Sunday: No Classes\n" }


        };


        public static string processAttendance(int inputProcess, string studentIdInput)
        {


            if (!validIds.TryGetValue(studentIdInput, out string studentName))
            {


                return "Invalid ID.";


            }


            switch (inputProcess)
            {


                case 1:


                    return $"Welcome {studentName}";




                case 2:


                    return $"Goodbye {studentName}";




                case 3:


                    return attendances.ContainsKey(studentIdInput) ? attendances[studentIdInput] : "No schedule found.";



                case 4:


                    Random random = new Random();

                    int lates = random.Next(1, 10);
                    int absents = random.Next(1, 10);


                    return $"Number of Lates: {lates}\nNumber of Absents: {absents}";


                default:


                    return "Invalid process selection.";


            }


        }
                    //public static void processAttendance(int inputProcess, string studentIdInput)
                    //{


                    //    string id1 = "2023-0001", id2 = "2024-0002", id3 = "2024-0003";
                    //    string name1 = "Yves", name2 = "JM", name3 = "Alfred";


                    //    /*
                    //     * choosing what proccess should be done/display
                    //     * case 1 for clock in
                    //     * case 2 for clock out
                    //     */
                    //    switch (inputProcess)
                    //    {


                    //        case 1:


                    //            //for checking each id and displaying name
                    //            if (studentIdInput == id1)
                    //            {


                    //                Console.WriteLine($"Welcome {name1}");


                    //            }


                    //            else if (studentIdInput == id2)
                    //            {


                    //                Console.WriteLine($"Welcome {name2}");


                    //            }


                    //            else if (studentIdInput == id3)
                    //            {


                    //                Console.WriteLine($"Welcome {name3}");


                    //            }


                    //            break;


                    //        case 2:


                    //            if (studentIdInput == id1)
                    //            {


                    //                Console.WriteLine($"Goodbye {name1}");


                    //            }


                    //            else if (studentIdInput == id2)
                    //            {


                    //                Console.WriteLine($"Goodbye {name2}");


                    //            }


                    //            else if (studentIdInput == id3)
                    //            {


                    //                Console.WriteLine($"Goodbye {name3}");


                    //            }


                    //            break;


                    //        case 3:

                    //            //temporary stored in string and same set of schedule on all ID
                    //            if (studentIdInput == id1 || studentIdInput == id2 || studentIdInput == id3)
                    //            {


                    //                Console.WriteLine("\n--- Class Schedule ---");
                    //                Console.WriteLine("Monday - Friday: 8:00 AM - 5:00 PM");
                    //                Console.WriteLine("Saturday: 9:00 AM - 2:00 PM");
                    //                Console.WriteLine("Sunday: No Classes\n");


                    //            }

                    //            break;


                    //        case 4:

                    //            //provide random number for lates and absents
                    //            Random random = new Random();
                    //            int randomNumber1 = random.Next(1, 10);
                    //            int randomNumber2 = random.Next(1, 10);


                    //            if (studentIdInput == id1)
                    //            {


                    //                Console.WriteLine($"Number of Lates: {randomNumber1}");
                    //                Console.WriteLine($"Number of Absents: {randomNumber2}");


                    //            }


                    //            else if (studentIdInput == id2)
                    //            {


                    //                Console.WriteLine($"Number of Lates: {randomNumber1}");
                    //                Console.WriteLine($"Number of Absents: {randomNumber2}");


                    //            }


                    //            else if (studentIdInput == id3)
                    //            {


                    //                Console.WriteLine($"Number of Lates: {randomNumber1}");
                    //                Console.WriteLine($"Number of Absents: {randomNumber2}");


                    //            }


                    //            break;


                    //    }


                    //}
            }
}
