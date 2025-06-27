using IDSystemBusinessLogic;
using IDSystemData;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IDSystemGUI
{

    public static class Displays
    {


        //display student info
        public static void displayStudentInfo()
        {


            //mark all absents
            Checking.CheckAllAbsents();



            var name = Checking.getStudentName();
            Console.WriteLine($"\nHello, {name}!");


            //chck if in or out 
            Console.WriteLine(Checking.InOrOut());


            //display schedule
            var schedule = Checking.getSchedule();
            if (string.IsNullOrWhiteSpace(schedule) || !schedule.Contains(DateTime.Today.DayOfWeek.ToString(), StringComparison.OrdinalIgnoreCase))
            {


                Console.WriteLine("You have no scheduled class today.");


            }


            else
            {


                Console.WriteLine(schedule);


            }


            //show lates and absents
            var (lates, absents) = Checking.getRecord();
            Console.WriteLine($"\nCurrent Record -- Lates: {lates}, Absents: {absents}");


        }



        //get student ID
        public static string StudentId
        {


            get
            {


                while (true)
                {


                    Console.Write("Enter Your ID Number: ");
                    var input = Console.ReadLine().Trim();


                    //exit the loop
                    if (input.Equals("ex", StringComparison.OrdinalIgnoreCase))
                        return input;


                    //show admin 
                    if (input.Equals("admin", StringComparison.OrdinalIgnoreCase))
                    {


                        adminDashboard();
                        continue;


                    }


                    //check ID
                    if (Checking.checkId(input))
                    {


                        Checking.setCurrentID(input);
                        return input;


                    }


                    Console.WriteLine("ID not found. Please try again.\n");


                }


            }


        }



        //admin dashboard
        public static void adminDashboard()
        {


            Console.WriteLine("\nAdmin Dashboard\nWelcome Admin");
            bool exit = false;

            //loop admin until exit
            while (!exit)
            {


                //admin options
                Console.WriteLine("\n1. Add Student");
                Console.WriteLine("2. Edit Schedule");
                Console.WriteLine("3. Delete Student");
                Console.WriteLine("4. List All IDs");
                Console.WriteLine("5. Exit Admin Mode");
                Console.Write("CChoose from 1 to 5> ");


                switch (Console.ReadLine().Trim())
                {


                    case "1": adminAddNewStudent(); break;
                    case "2": adminEditSchedule(); break;
                    case "3": adminDeleteStudent(); break;
                    case "4": adminListAllStudent(); break;
                    case "5": exit = true; break;
                    default: Console.WriteLine("Invalid number."); break;


                }


            }


            Console.WriteLine("Exiting Admin Mode.\n");


        }



        //add new student
        private static void adminAddNewStudent()
        {


            Console.Write("New ID (0000-0000): ");
            var id = Console.ReadLine().Trim();


            Console.Write("Name: ");
            var name = Console.ReadLine().Trim();


            //how many schedule for student
            Console.Write("How many schedule entries for this student?(1-6)> ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n < 1) n = 1;


            var list = new List<string>();
            for (int i = 0; i < n; i++)
            {


                Console.Write($"Day (ex. Monday): ");
                var day = Console.ReadLine().Trim();
                Console.Write("Start (h:mm tt): ");
                var start = Console.ReadLine().Trim();
                Console.Write("End   (h:mm tt): ");
                var end = Console.ReadLine().Trim();
                list.Add($"{day}: {start} - {end}");


            }


            bool ifAddIsCorrect = Checking.adminAdd(id, name, list);
            Console.WriteLine(ifAddIsCorrect ? "Student added successfully." : "Add failed (duplicate ID?).");


        }



        //edit schedule
        private static void adminEditSchedule()
        {

            Console.Write("Enter the ID for schedule update: ");
            var id = Console.ReadLine().Trim();


            if (!Checking.checkId(id))
            {


                Console.WriteLine("Student not found.");
                return;


            }


            //set the current ID for schedule editing
            Checking.setCurrentID(id);


            //check if the student has a schedule
            var schedToDisplay = Checking.getSchedule();


            //split the schedule into lines and filter out empty lines and dashes
            var listOfSchedules = schedToDisplay.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(line => line.Trim()).Where(line => !string.IsNullOrWhiteSpace(line)).Where(line => !line.StartsWith("---")).ToArray();


            if (listOfSchedules.Length == 0)
            {


                Console.WriteLine("No schedule to edit.");
                return;


            }


            //show schedules
            Console.WriteLine("\nCurrent Schedule:");
            for (int i = 0; i < listOfSchedules.Length; i++)
                Console.WriteLine($"{i + 1}. {listOfSchedules[i]}");


            Console.Write("\nSelect the entry number to edit: ");
            if (!int.TryParse(Console.ReadLine().Trim(), out int choice) || choice < 1 || choice > listOfSchedules.Length)
            {


                Console.WriteLine("Invalid selection.");
                return;


            }


            //get the selected entry
            string oldSchedule = listOfSchedules[choice - 1];
            int colon = oldSchedule.IndexOf(':');
            string day = colon > 0 ? oldSchedule.Substring(0, colon).Trim() : oldSchedule;


            //create time
            Console.Write($"New Start Time for {day} (h:mm tt): ");
            string newStart = Console.ReadLine().Trim();
            Console.Write($"New End   Time for {day} (h:mm tt): ");
            string newEnd = Console.ReadLine().Trim();


            // Rebuild that line
            string newSchedule = $"{day}: {newStart} - {newEnd}";
            listOfSchedules[choice - 1] = newSchedule;


            //update is ok
            bool ifUpadateIsOk = Checking.adminUpdate(id, listOfSchedules.ToList());
            Console.WriteLine(ifUpadateIsOk ? "Schedule updated successfully." : "Failed to update schedule.");


        }



        //delete student
        private static void adminDeleteStudent()
        {

            Console.Write("ID to delete: ");
            var id = Console.ReadLine().Trim();


            bool ifDeleteIsOk = Checking.adminDelete(id);
            Console.WriteLine(ifDeleteIsOk ? "Deleted." : "Delete failed (ID not found).");


        }



        //list all students
        private static void adminListAllStudent()
        {


            Console.WriteLine("\nAll Students:");
            foreach (var (sid, nm) in Checking.AdminListAll())
                Console.WriteLine($"  {sid} : {nm}");


        }


 
    }


}