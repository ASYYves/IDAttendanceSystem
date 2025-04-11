using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDSystemData;

namespace IDSystemBusinessLogic
{
    public class Checking
    {

        //getting student id
        public static string studentId 
        {
            

            get; 
            set; 
        

        }


        public static string studentName =>
            Data.validIds.TryGetValue(studentId, out string name) ? name : "who you";


        //check ID valid
        public static bool checkId(string studentIdInput)
        {


            return Data.validIds.ContainsKey(studentIdInput);


        }


        //process schedule
        public static string getSched()
        {


            return Data.attendances.TryGetValue(studentId, out string schedule) ? schedule : "no sched";


        }


        //create random value for late and absent
        public static string getRecord()
        {


            Random random = new Random();
            int lates = random.Next(1, 10);
            int absents = random.Next(1, 10);


            return
                $"Lates: {lates}\n" +
                $"Absents: {absents}";


        }


        //for storing In or Out
        private static readonly Dictionary<string, bool> clockInorOut = new Dictionary<string, bool>();


        //process if In or Out
        public static string inOrout()
        {


            bool ifClockIn = false;

            //check if ID is valid
            if (clockInorOut.TryGetValue(studentId, out ifClockIn))
            {


                //if clock out
                clockInorOut[studentId] = !ifClockIn;


            }


            else
            {


                //if clock in
                clockInorOut[studentId] = true;


            }


            //display message 
            return clockInorOut[studentId] ? "You are clocked in.\n" : "You are clocked out.\n";


        }

        
    }
}
