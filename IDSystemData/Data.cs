using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using JsonFormatting = Newtonsoft.Json.Formatting;

namespace IDSystemData
{

    namespace IDSystemData
    {


        public class dbStorage : studentStorageLocation, attendanceStorageLocation
        {


            private readonly DBData db = new DBData();



            //if DB exists, load students and attendance logs
            public bool checkIfFormatOfStorage(string studentId) => db.LoadStudents().ContainsKey(studentId);

            public string getName(string studentId) => db.getStudentData(studentId).Name;

            public string getSchedule(string studentId)
            {


                var schedules = db.LoadSchedules();
                return schedules.TryGetValue(studentId, out var sched) ? sched : string.Empty;


            }

            public bool addStudentToStorage(string studentId, string name, List<string> schedule) => db.addNewStudentToDB(studentId, name, string.Join("\n", schedule));

            public bool updateSchduleToStorage(string studentId, List<string> newSchedule) => db.updateSchedule(studentId, string.Join("\n", newSchedule));

            public bool deleteScheduleToStorage(string studentId) => db.deleteStudentFromDB(studentId);

            public List<(string StudentId, string Name)> listAllStudentsFromStorage() => db.listAllIDs();

            public void logAttendanceToStorage(string studentId, bool isClockIn) => db.logAttendanceOfStudents(studentId, isClockIn);

            public List<(DateTime Timestamp, bool IsClockIn)> getLogsFromStorage(string studentId) => db.getAttendanceLogs(studentId);

            public (int Lates, int Absents) getRecordsFromStorage(string studentId)
            {


                var rec = db.getStudentData(studentId);
                return (rec.Lates, rec.Absents);


            }

            public void addLatesToStorage(string studentId) => db.addLates(studentId);

            public void addAbsentsToStorage(string studentId) => db.IncrementAbsents(studentId);


        }


        

        /// <summary>Master data: students + schedules</summary>
        public interface studentStorageLocation
        {
            bool checkIfFormatOfStorage(string studentId);
            string getName(string studentId);
            string getSchedule(string studentId);               // multi‐line string
            bool addStudentToStorage(string studentId, string name, List<string> schedule);
            bool updateSchduleToStorage(string studentId, List<string> newSchedule);
            bool deleteScheduleToStorage(string studentId);
            List<(string StudentId, string Name)> listAllStudentsFromStorage();
        }

        /// <summary>Attendance log: clock‐in/out & counts</summary>
        public interface attendanceStorageLocation
        {
            void logAttendanceToStorage(string studentId, bool isClockIn);
            List<(DateTime Timestamp, bool IsClockIn)> getLogsFromStorage(string studentId);
            (int Lates, int Absents) getRecordsFromStorage(string studentId);
            void addLatesToStorage(string studentId);
            void addAbsentsToStorage(string studentId);
        }

        

        

        public static class storageFormat
        {
            public static studentStorageLocation selectStudentStorage(string mode, string path)
            {
                if (mode == "db")
                    return new dbStorage();        //change here

                if (mode.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    return new jsonStudentStorage(path);

                if (mode.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                    return new txtStudentStorage(path);

                throw new ArgumentException($"Unknown student mode: {mode}");
            }

            public static attendanceStorageLocation selectAttendanceStorage(string mode, string path)
            {
                if (mode == "db")
                    return new dbStorage();        //and change here

                if (mode.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    return new jsonAttendanceStorage(path);

                if (mode.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                    return new txtAttendanceStore(path);

                throw new ArgumentException($"Unknown attendance mode: {mode}");
            }
        }



        //txt storage implementations for students
        public class txtStudentStorage : studentStorageLocation
        {


            private readonly string file;

            public txtStudentStorage(string filePath) => file = filePath;

            public bool checkIfFormatOfStorage(string studentId) => File.Exists(file) && File.ReadLines(file).Any(l => l.Split(',')[0] == studentId);

            public string getName(string studentId)
            {

                if (!checkIfFormatOfStorage(studentId)) 
                    return "";


                return File.ReadLines(file).First(l => l.Split('|')[0] == studentId).Split('|')[1];


            }

            public string getSchedule(string studentId)
            {


                if (!checkIfFormatOfStorage(studentId)) 
                    return "";


                var parts = File.ReadLines(file).First(l => l.Split('|')[0] == studentId).Split('|', 3);
                return parts.Length < 3 ? "" : parts[2].Replace(';', '\n');
            }

            public bool addStudentToStorage(string studentId, string name, List<string> schedule)
            {


                if (checkIfFormatOfStorage(studentId)) 
                    return false;


                var line = $"{studentId},{name},{string.Join(';', schedule)}";
                File.AppendAllLines(file, new[] { line });
                return true;


            }

            public bool updateSchduleToStorage(string studentId, List<string> newSchedule)
            {


                if (!checkIfFormatOfStorage(studentId)) 
                    return false;


                var all = File.ReadAllLines(file).ToList();


                for (int i = 0; i < all.Count; i++)
                {


                    var p = all[i].Split(',', 3);
                    if (p[0] == studentId)
                    {


                        all[i] = $"{studentId},{p[1]},{string.Join(';', newSchedule)}";
                        break;


                    }


                }


                File.WriteAllLines(file, all);
                return true;


            }

            public bool deleteScheduleToStorage(string studentId)
            {

                if (!checkIfFormatOfStorage(studentId)) 
                    return false;


                var keep = File.ReadAllLines(file).Where(lines => lines.Split(',')[0] != studentId);
                File.WriteAllLines(file, keep);
                return true;


            }

            public List<(string StudentId, string Name)> listAllStudentsFromStorage() => File.Exists(file) ? File.ReadAllLines(file).Select(l => l.Split('|')).Where(p => p.Length >= 2).Select(p => (p[0], p[1])).ToList() : new();


        }


        //txt storage implementation for attendance logs
        public class txtAttendanceStore : attendanceStorageLocation
        {
            private readonly string file;

            public txtAttendanceStore(string filePath) => file = filePath;

            public void logAttendanceToStorage(string studentId, bool isClockIn) => File.AppendAllText(file, $"{studentId},{isClockIn},{DateTime.Now:O}\n");

            public List<(DateTime Timestamp, bool IsClockIn)> getLogsFromStorage(string studentId)
            {


                if (!File.Exists(file)) 
                    return new();


                return File.ReadAllLines(file).Select(l => l.Split(',')).Where(p => p.Length == 3 && p[0] == studentId).Select(p => (Timestamp: DateTime.Parse(p[2], null, DateTimeStyles.RoundtripKind), IsClockIn: bool.Parse(p[1]))).ToList();


            }


            public (int Lates, int Absents) getRecordsFromStorage(string studentId)
            {


                var lines = getLogsFromStorage(studentId);
                int lates = lines.Count(l => !l.IsClockIn);
                return (lates, 0);


            }

            public void addLatesToStorage(string studentId) => logAttendanceToStorage(studentId, false);

            public void addAbsentsToStorage(string studentId) => logAttendanceToStorage(studentId, false);


        }



        //json storage implementations for students
        public class jsonStudentStorage : studentStorageLocation
        {


            private readonly string file;
            private List<StudentRecord> data;

            public jsonStudentStorage(string filePath)
            {


                file = filePath;
                if (File.Exists(file))
                    data = JsonConvert.DeserializeObject<List<StudentRecord>>(File.ReadAllText(file)) ?? new();


                else


                    data = new();


            }

            public bool checkIfFormatOfStorage(string studentId) => data.Any(s => s.Id == studentId);

            public string getName(string studentId) => data.FirstOrDefault(s => s.Id == studentId)?.Name ?? "";

            public string getSchedule(string studentId) => data.FirstOrDefault(s => s.Id == studentId) is var rec && rec != null ? string.Join("\n", rec.Schedule) : "";

            public bool addStudentToStorage(string studentId, string name, List<string> schedule)
            {


                if (checkIfFormatOfStorage(studentId)) 
                    return false;


                data.Add(new StudentRecord
                {


                    Id = studentId,
                    Name = name,
                    Schedule = schedule


                });


                saveChanges();
                return true;


            }

            public bool updateSchduleToStorage(string studentId, List<string> newSchedule)
            {


                var rec = data.FirstOrDefault(s => s.Id == studentId);
                if (rec == null) 
                    return false;


                rec.Schedule = newSchedule;
                saveChanges();
                return true;


            }

            public bool deleteScheduleToStorage(string studentId)
            {


                var rec = data.FirstOrDefault(s => s.Id == studentId);
                if (rec == null) 
                    return false;


                data.Remove(rec);
                saveChanges();
                return true;


            }

            public List<(string StudentId, string Name)> listAllStudentsFromStorage() => data.Select(s => (s.Id, s.Name)).ToList();

            private void saveChanges() => File.WriteAllText(file, JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented));

            private class StudentRecord
            {


                public string Id { get; set; }
                public string Name { get; set; }
                public List<string> Schedule { get; set; }


            }


        }




        public class jsonAttendanceStorage : attendanceStorageLocation
        {
            private readonly string file;
            private Dictionary<string, List<LogEntry>> data;

            public jsonAttendanceStorage(string filePath)
            {


                file = filePath;
                if (File.Exists(file))
                    data = JsonConvert.DeserializeObject<Dictionary<string, List<LogEntry>>>(File.ReadAllText(file)) ?? new();


                else
                    data = new();


            }

            public void logAttendanceToStorage(string studentId, bool isClockIn)
            {


                if (!data.ContainsKey(studentId))
                    data[studentId] = new();


                data[studentId].Add(new LogEntry
                {


                    Timestamp = DateTime.Now,
                    IsClockIn = isClockIn


                });


                saveChanges();


            }

            public List<(DateTime Timestamp, bool IsClockIn)> getLogsFromStorage(string studentId) => data.TryGetValue(studentId, out var list) ? list.Select(e => (e.Timestamp, e.IsClockIn)).ToList() : new();

            public (int Lates, int Absents) getRecordsFromStorage(string studentId) => (data.TryGetValue(studentId, out var l) ? l.Count(e => !e.IsClockIn) : 0, 0);

            public void addLatesToStorage(string studentId) => logAttendanceToStorage(studentId, false);

            public void addAbsentsToStorage(string studentId) => logAttendanceToStorage(studentId, false);

            private void saveChanges() => File.WriteAllText(file, JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented));

            private class LogEntry
            {


                public DateTime Timestamp { get; set; }
                public bool IsClockIn { get; set; }


            }
        }



    }
    
    
}