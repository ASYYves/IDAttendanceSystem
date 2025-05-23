using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using JsonFormatting = Newtonsoft.Json.Formatting;

namespace IDSystemData
{

    public static class DataStorage
    {


        public static Dictionary<string, string> validIDs = new();
        public static Dictionary<string, string> Attendances = new();
        public static loggingAttendanceToFile logAttendanceofStudents;


    }


    public interface loggingAttendanceToFile
    {


        void LogAttendance(string studentId, bool clockedIn);


        recordOfAttendance GetAttendance(string studentId);


    }


    public class recordOfAttendance
    {


        public int Lates { get; set; }
        public int Absents { get; set; }


    }


    public class storeAttendancetoTXT : loggingAttendanceToFile
    {


        private readonly string filePath;


        public storeAttendancetoTXT(string path)
        {


            filePath = path;


        }


        public void LogAttendance(string studentId, bool clockedIn)
        {


            File.AppendAllText(filePath, $"{studentId},{clockedIn},{DateTime.Now}\n");


        }


        public recordOfAttendance GetAttendance(string studentId)
        {


            var record = new recordOfAttendance();


            if (File.Exists(filePath))
            {


                var lines = File.ReadAllLines(filePath);


                foreach (var line in lines)
                {


                    var parts = line.Split(',');


                    if (parts[0] == studentId)
                    {


                        if (bool.TryParse(parts[1], out bool isIn) && !isIn)
                        {


                            record.Lates++;


                        }


                    }


                }


            }


            return record;


        }


    }


    public class storeAttendanceToJSON : loggingAttendanceToFile
    {


        private readonly string filePath;


        private Dictionary<string, recordOfAttendance> attendanceData = new();


        public storeAttendanceToJSON(string path)
        {


            filePath = path;


            if (File.Exists(filePath))
            {


                string json = File.ReadAllText(filePath);
                attendanceData = JsonConvert.DeserializeObject<Dictionary<string, recordOfAttendance>>(json) ?? new();


            }


        }


        public void LogAttendance(string studentId, bool clockedIn)
        {


            if (!attendanceData.ContainsKey(studentId))
            {


                attendanceData[studentId] = new recordOfAttendance();


            }


            if (!clockedIn)
            {


                attendanceData[studentId].Lates++;


            }


            File.WriteAllText(filePath, JsonConvert.SerializeObject(attendanceData, JsonFormatting.Indented));


        }


        public recordOfAttendance GetAttendance(string studentId)
        {


            return attendanceData.TryGetValue(studentId, out var record) ? record : new recordOfAttendance();


        }


    }


    public static class Data
    {

        public static void LoadStudentData(string filePath)
        {


            if (!File.Exists(filePath)) return;


            if (filePath.EndsWith(".json"))
            {


                var json = File.ReadAllText(filePath);
                var students = JsonConvert.DeserializeObject<List<Student>>(json);


                if (students != null)
                {


                    foreach (var student in students)
                    {


                        DataStorage.validIDs[student.Id] = student.Name;
                        DataStorage.Attendances[student.Id] = $"\n--- Schedule ---\n{student.Schedule}\n";


                    }


                }


            }


            else
            {


                var lines = File.ReadAllLines(filePath);


                foreach (var line in lines)
                {


                    //for json running
                    //var parts = line.Split(',');
                    //if (parts.Length == 2)
                    //{


                    //    DataStorage.validIDs[parts[0]] = parts[1];
                    //}


                    //for txt running
                    var parts = line.Split('|');


                    if (parts.Length >= 2)
                    {


                        string id = parts[0].Trim();
                        string name = parts[1].Trim();
                        DataStorage.validIDs[id] = name;


                        if (parts.Length >= 3)
                        {


                            string timeIn = parts[2].Trim();
                            DataStorage.Attendances[id] = $"\n--- Schedule ---\nTime In: {timeIn}";


                        }


                    }


                }


            }


        }


        public class Student
        {


            public string Id { get; set; }
            public string Name { get; set; }
            public string Schedule { get; set; }


        }


    }


}

