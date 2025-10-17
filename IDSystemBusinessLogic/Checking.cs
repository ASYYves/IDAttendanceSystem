using IDSystemData;
using IDSystemData.IDSystemData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDSystemBusinessLogic
{

    public static class Checking
    {


        ///locations for students and attendance logs
        static studentStorageLocation storingStudents;
        static attendanceStorageLocation storingAttendances;



        //id of the student currently being processed
        public static string currentIDThatIsProcessed { get; private set; }



        //set the storage locations for students and attendances
        public static void setStorageLocation(studentStorageLocation studentStore, attendanceStorageLocation attendanceStore)
        {

            storingStudents = studentStore;
            storingAttendances = attendanceStore;

        }



        //check if the ID exists in the student storage
        public static bool checkId(string id) => storingStudents.checkIfFormatOfStorage(id);



        //check if the ID exists in the attendance storage
        public static void setCurrentID(string id) => currentIDThatIsProcessed = id;



        //get name of the student by ID
        public static string getStudentName() => storingStudents.getName(currentIDThatIsProcessed);



        // get the schedule of the student by ID
        public static string getSchedule() => storingStudents.getSchedule(currentIDThatIsProcessed);


        private static Email email = new Email();


        //chck if student is in or out
        public static string InOrOut()
        {


            var logs = storingAttendances.getLogsFromStorage(currentIDThatIsProcessed);
            bool isIn = logs.LastOrDefault().IsClockIn && logs.Last().Timestamp.Date == DateTime.Today;


            if (!isIn)
            {


                checkIfStudentIsLate();
                storingAttendances.logAttendanceToStorage(currentIDThatIsProcessed, true);
                email.SendEmail("Yves", "2023-0001");
                return "You are clocked in.";


            }


            else
            {


                storingAttendances.logAttendanceToStorage(currentIDThatIsProcessed, false);
                return "Goodbye! You are clocked out.";


            }


        }



        //check if the student is late based on today’s schedule
        static void checkIfStudentIsLate()
        {

            //get today’s schedule line for the current student
            var todayLine = getSchedule().Split('\n', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault(line =>line.Contains(DateTime.Today.DayOfWeek.ToString(),StringComparison.OrdinalIgnoreCase));
            if (todayLine == null) return;


            //parse the time part and check if it’s past the start time + 15 minutes
            var timePart = todayLine.Split(':', 2)[1].Split('-', 2)[0].Trim();
            if (DateTime.TryParse(timePart, out var start) && DateTime.Now.TimeOfDay > start.TimeOfDay.Add(TimeSpan.FromMinutes(15)))
            {


                storingAttendances.addLatesToStorage(currentIDThatIsProcessed);


            }


        }



        //mark all students who are absent today
        public static void CheckAllAbsents()
        {


            var today = DateTime.Today;
            foreach (var (id, _) in storingStudents.listAllStudentsFromStorage())
            {

                //skip if any log today
                if (storingAttendances.getLogsFromStorage(id).Any(l => l.Timestamp.Date == today)) continue;


                var line = storingStudents.getSchedule(id).Split('\n', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault(lines =>lines.Contains(today.DayOfWeek.ToString(),StringComparison.OrdinalIgnoreCase));
                if (line == null) continue;


                var timePart = line.Split(':', 2)[1].Split('-', 2)[0].Trim();
                if (DateTime.TryParse(timePart, out var start) && DateTime.Now.TimeOfDay > start.TimeOfDay.Add(TimeSpan.FromMinutes(15)))
                {


                    storingAttendances.addAbsentsToStorage(id);


                }


            }


        }



        //get the attendance record for the current student
        public static (int Lates, int Absents) getRecord() => storingAttendances.getRecordsFromStorage(currentIDThatIsProcessed);



        // Admin operations
        public static bool adminAdd(string id, string name, List<string> sched) => storingStudents.addStudentToStorage(id, name, sched);

        public static bool adminUpdate(string id, List<string> sched) => storingStudents.updateSchduleToStorage(id, sched);

        public static bool adminDelete(string id) => storingStudents.deleteScheduleToStorage(id);

        public static List<(string StudentId, string Name)> AdminListAll() => storingStudents.listAllStudentsFromStorage();



        //automatically clock out all students who are still clocked in after the scheduled end time + grace period
        public static void AutoClockOutAll(TimeSpan gracePeriod)
        {


            // for every student, if still clocked in past end+grace, auto log out
            foreach (var (id, _) in storingStudents.listAllStudentsFromStorage())
            {


                //skip if no logs today or not clocked in
                var logs = storingAttendances.getLogsFromStorage(id);
                if (logs.Count == 0) continue;


                //only care if they’re currently clocked in today
                var last = logs.Last();
                if (!last.IsClockIn || last.Timestamp.Date != DateTime.Today)
                    continue;


                //find today’s schedule line
                var todayLine = storingStudents.getSchedule(id).Split('\n', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault(l => l.Contains(DateTime.Today.DayOfWeek.ToString(), StringComparison.OrdinalIgnoreCase));
                if (todayLine == null) continue;


                //parse the end-time part
                var endPart = todayLine.Split(':', 2)[1].Split('-', 2)[1].Trim();
                if (!DateTime.TryParse(endPart, out var endTime))
                    continue;


                //build a DateTime for today’s scheduled end
                var scheduledEnd = DateTime.Today.Add(endTime.TimeOfDay);


                if (DateTime.Now > scheduledEnd.Add(gracePeriod))
                {


                    // auto log‐out
                    storingAttendances.logAttendanceToStorage(id, false);


                }


            }


        }

        



    }

        
}