using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using IDSystemData;

namespace IDSystemBusinessLogic
{


    public class Checking
    {


        public static string studentId { get; set; }


        public static string studentName 
            => DataStorage.validIDs.TryGetValue(studentId, out string name) ? name : "Unknown";


        public static bool checkId(string studentIdInput)
        {


            return DataStorage.validIDs.ContainsKey(studentIdInput);


        }


        public static string getSched()
        {


            return DataStorage.Attendances.TryGetValue(studentId, out string schedule) ? schedule : "no sched";


        }


        public static string getRecord()
        {


            var recordAttendance = DataStorage.logAttendanceofStudents.GetAttendance(studentId);
            return $"Lates: {recordAttendance.Lates}\nAbsents: {recordAttendance.Absents}";


        }


        private static readonly Dictionary<string, bool> clokingINorOUT = new Dictionary<string, bool>();


        public static string inOrout()
        {


            var ifClockIN = false;


            if (clokingINorOUT.TryGetValue(studentId, out ifClockIN))
            {


                clokingINorOUT[studentId] = !ifClockIN;


            }


            else
            {


                clokingINorOUT[studentId] = true;


            }


            var ifIN = clokingINorOUT[studentId];
            var action = ifIN ? "clocked in" : "clocked out";


            DataStorage.logAttendanceofStudents.logAttendanceOfStudents(studentId, ifIN);


            return ifIN ? "You are clocked in." : "Goodbye! You are clocked out.";


        }


        public static void setAttendanceOfStudents(string path)
        {


            if (path.EndsWith(".json"))
            {


                DataStorage.logAttendanceofStudents = new storeAttendanceToJSON(path);


            }


            else if (path == "db")
            {


                DataStorage.logAttendanceofStudents = new DBData();


            }


            else
            {


                DataStorage.logAttendanceofStudents = new storeAttendancetoTXT(path);


            }


        }


    }


}
