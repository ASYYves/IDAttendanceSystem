using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace IDSystemData
{

    public interface loggingAttendanceToFile
    {
        void logAttendanceOfStudents(string studentId, bool clockedIn);
        recordOfAttendance getAttendance(string studentId);
        List<(DateTime timestamp, bool isClockIn)> getAttendanceLogs(string studentId);
    }

    public class recordOfAttendance
    {
        public int Lates { get; set; }
        public int Absents { get; set; }
    }


    //hold all the student data
    public class StudentDataRecord
    {


        public string Name { get; set; }


        public int Lates { get; set; }


        public int Absents { get; set; }


    }



    public class DBData : loggingAttendanceToFile
    {


        private string connectionString = "Data Source=DESKTOP-B8KUMH6\\SQLEXPRESS;Initial Catalog=ID_System;Integrated Security=True;TrustServerCertificate=True;";



        //loads the names and IDs for the initial check
        public Dictionary<string, string> LoadStudents()
        {


            var students = new Dictionary<string, string>();
            

                using var conn = new SqlConnection(connectionString);
                var query = "SELECT Id, Name FROM Students";
                var cmd = new SqlCommand(query, conn);
                conn.Open();


                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {


                    students[reader["Id"].ToString()] = reader["Name"].ToString();


                }


                conn.Close();


            return students;


        }



        //gets the Name, Lates, and Absents for one student
        public StudentDataRecord getStudentData(string studentId)
        {


            var record = new StudentDataRecord { Name = "Unknown", Lates = 0, Absents = 0 };
            using var conn = new SqlConnection(connectionString);
            var query = "SELECT Name, Lates, Absents FROM Students WHERE Id = @StudentId";
            var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@StudentId", studentId);
            conn.Open();


            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {


                 record.Name = reader["Name"].ToString();
                 record.Lates = (int)reader["Lates"];
                 record.Absents = (int)reader["Absents"];


            }


            conn.Close();


            return record;


        }



        //increments the 'Lates' count for a student
        public void addLates(string studentId)
        {
            

            using var conn = new SqlConnection(connectionString);
            var query = "UPDATE Students SET Lates = Lates + 1 WHERE Id = @StudentId";
            var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@StudentId", studentId);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();


        }



        //increments the 'Absents' count for a student
        public void IncrementAbsents(string studentId)
        {


            using var conn = new SqlConnection(connectionString);
            var cmd = new SqlCommand("UPDATE Students SET Absents = Absents + 1 WHERE Id = @id", conn);
            cmd.Parameters.AddWithValue("@id", studentId);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();


        }



        //logs the attendance of students in the database
        public void logAttendanceOfStudents(string studentId, bool clockedIn)
        {
            

            using var conn = new SqlConnection(connectionString);
            var query = "INSERT INTO StudentAttendanceLogs (StudentId, LogTime, IsClockIn) VALUES (@StudentId, @LogTime, @IsClockIn)";
            var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@StudentId", studentId);
            cmd.Parameters.AddWithValue("@LogTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@IsClockIn", clockedIn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();


        }



        //gets the attendance logs for a student
        public List<(DateTime timestamp, bool isClockIn)> getAttendanceLogs(string studentId)
        {
            var logs = new List<(DateTime, bool)>();
            
            using var conn = new SqlConnection(connectionString);
            string query = "SELECT LogTime, IsClockIn FROM StudentAttendanceLogs WHERE StudentId = @StudentId";
            var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@StudentId", studentId);
            conn.Open();
            using var reader = cmd.ExecuteReader();


             while (reader.Read())
             {


                logs.Add(((DateTime)reader["LogTime"], (bool)reader["IsClockIn"]));


             }
            

            conn.Close();


            return logs;


        }



        //loads the schedules for all students from the database
        public Dictionary<string, string> LoadSchedules()
        {


            var schedules = new Dictionary<string, string>();
            using var conn = new SqlConnection(connectionString);
            var query = @"SELECT StudentId, DayOfWeek, StartTime, EndTime FROM StudentSchedules ORDER BY StudentId, CASE DayOfWeek WHEN 'Monday' THEN 1 WHEN 'Tuesday' THEN 2 WHEN 'Wednesday' THEN 3 WHEN 'Thursday' THEN 4 WHEN 'Friday' THEN 5 WHEN 'Saturday' THEN 6 WHEN 'Sunday' THEN 7 END";
            var cmd = new SqlCommand(query, conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            string currentStudent = null;
            var sb = new StringBuilder();


            while (reader.Read())
            {


                string studentId = reader["StudentId"].ToString();
                if (studentId != currentStudent)
                {


                    if (currentStudent != null)
                            schedules[currentStudent] = sb.ToString();
                        currentStudent = studentId;
                        sb.Clear();
                        sb.Append("\n--- Schedule ---\n");


                }


                string day = reader["DayOfWeek"].ToString();
                TimeSpan? start = reader["StartTime"] == DBNull.Value ? null : (TimeSpan?)reader["StartTime"];
                TimeSpan? end = reader["EndTime"] == DBNull.Value ? null : (TimeSpan?)reader["EndTime"];


                if (start == null || end == null)
                        sb.AppendLine($"{day}: No classes");


                else
                        sb.AppendLine($"{day}: {DateTime.Today.Add(start.Value):hh:mm tt} - {DateTime.Today.Add(end.Value):hh:mm tt}");


            }


            if (currentStudent != null)
                schedules[currentStudent] = sb.ToString();


            conn.Close();


            return schedules;


        }



        //gets the attendance record for a student
        public recordOfAttendance getAttendance(string studentId)
        {
            

            return new recordOfAttendance();


        }



        //adds a new student to the database along with their schedule
        public bool addNewStudentToDB(string studentId, string name, string schedule)
        {


            bool success = false;
            using (var conn = new SqlConnection(connectionString))
            {


                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {


                    try
                    {
                        

                        string insertStudentQuery = @"INSERT INTO Students (Id, Name, Lates, Absents) VALUES (@Id, @Name, 0, 0);";
                        using (var cmd = new SqlCommand(insertStudentQuery, conn, trans))
                        {


                            cmd.Parameters.AddWithValue("@Id", studentId);
                            cmd.Parameters.AddWithValue("@Name", name);
                            cmd.ExecuteNonQuery();


                        }

                        
                        bool scheduleOk = insertSchedule(conn, trans, studentId, schedule);
                        if (!scheduleOk)
                        {


                            throw new Exception("Failed to insert schedule entries.");


                        }


                        trans.Commit();
                        success = true;


                    }


                    catch (Exception ex)
                    {


                        trans.Rollback();

                        
                    }


                }


                conn.Close();


            }


            
            return success;


        }



        //deletes a student from the database, including their attendance logs and schedule entries
        public bool deleteStudentFromDB(string studentId)
        {


            bool success = false;
            using (var conn = new SqlConnection(connectionString))
            {


                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {


                    try
                    {

                 
                        string deleteLogQuery = "DELETE FROM StudentAttendanceLogs WHERE StudentId=@StudentId;";
                        using (var cmd = new SqlCommand(deleteLogQuery, conn, trans))
                        {


                            cmd.Parameters.AddWithValue("@StudentId", studentId);
                            cmd.ExecuteNonQuery();


                        }


                        string deleteScheduleQuery = "DELETE FROM StudentSchedules WHERE StudentId=@StudentId;";
                        using (var cmd = new SqlCommand(deleteScheduleQuery, conn, trans))
                        {


                            cmd.Parameters.AddWithValue("@StudentId", studentId);
                            cmd.ExecuteNonQuery();


                        }

                        
                        string deleteStudentQuery = "DELETE FROM Students WHERE Id=@StudentId;";
                        using (var cmd = new SqlCommand(deleteStudentQuery, conn, trans))
                        {


                            cmd.Parameters.AddWithValue("@StudentId", studentId);
                            cmd.ExecuteNonQuery();


                        }


                        trans.Commit();
                        success = true;


                    }


                    catch (Exception ex)
                    {


                        trans.Rollback();
                        

                    }


                }


                conn.Close();
            }


            return success;


        }



        //lists all students from the database, returning their IDs and names
        public List<(string StudentId, string Name)> listAllIDs()
        {


            var list = new List<(string, string)>();
            using (var conn = new SqlConnection(connectionString))
            {
                

                conn.Open();
                string query = "SELECT Id, Name FROM Students ORDER BY Id;";
                using (var cmd = new SqlCommand(query, conn))
                {


                    using (var reader = cmd.ExecuteReader())
                    {


                        while (reader.Read())
                        {


                            string id = reader["Id"].ToString();
                            string name = reader["Name"].ToString();
                            list.Add((id, name));


                        }


                    }


                }


                conn.Close();


            }


            return list;


        }



        //inserts a student's schedule into the database
        private bool insertSchedule(SqlConnection conn, SqlTransaction trans, string studentId, string schedule)
        {


            bool success = true;


            try
            {


                string[] lines = schedule.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);


                foreach (string line in lines)
                {


                    
                    int colonIndex = line.IndexOf(':');
                    if (colonIndex == -1)
                        continue;


                    string day = line.Substring(0, colonIndex).Trim();
                    

                    string times = line.Substring(colonIndex + 1).Trim();
                    string[] timeParts = times.Split('-');
                    if (timeParts.Length != 2)
                        continue;


                    string startTimeStr = timeParts[0].Trim();
                    string endTimeStr = timeParts[1].Trim();


                    if (!DateTime.TryParseExact(startTimeStr, "h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startTime))
                    {


                        success = false;
                        continue;


                    }


                    if (!DateTime.TryParseExact(endTimeStr, "h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endTime))
                    {


                        success = false;
                        continue;


                    }


                    string insertScheduleQuery = @"INSERT INTO StudentSchedules (StudentId, DayOfWeek, StartTime, EndTime) VALUES (@StudentId, @Day, @StartTime, @EndTime);";
                    using (var cmd = new SqlCommand(insertScheduleQuery, conn, trans))
                    {


                        cmd.Parameters.AddWithValue("@StudentId", studentId);
                        cmd.Parameters.AddWithValue("@Day", day);
                        cmd.Parameters.AddWithValue("@StartTime", startTime.TimeOfDay);
                        cmd.Parameters.AddWithValue("@EndTime", endTime.TimeOfDay);
                        cmd.ExecuteNonQuery();


                    }


                }


            }


            catch (Exception ex)
            {
                

                success = false;


            }


            return success;


        }



        //updates a student's schedule in the database
        public bool updateSchedule(string studentId, string newSchedule)
        {

            bool success = true;


            using (var conn = new SqlConnection(connectionString))
            {


                conn.Open();
                using (var trans = conn.BeginTransaction())
                {


                    try
                    {
                        

                        var deleteQuery = "DELETE FROM StudentSchedules WHERE StudentId = @StudentId";
                        using (var deleteCmd = new SqlCommand(deleteQuery, conn, trans))
                        {


                            deleteCmd.Parameters.AddWithValue("@StudentId", studentId);
                            deleteCmd.ExecuteNonQuery();


                        }

                        

                        string[] lines = newSchedule.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var line in lines)
                        {


                            int colonIndex = line.IndexOf(':');
                            if (colonIndex == -1) continue;


                            string day = line.Substring(0, colonIndex).Trim();
                            string timeRange = line.Substring(colonIndex + 1).Trim();
                            var parts = timeRange.Split('-');
                            if (parts.Length != 2) 
                                continue;


                            if (DateTime.TryParse(parts[0].Trim(), out DateTime startTime) && DateTime.TryParse(parts[1].Trim(), out DateTime endTime))
                            {


                                string insertQuery = @"INSERT INTO StudentSchedules (StudentId, DayOfWeek, StartTime, EndTime) VALUES (@StudentId, @Day, @StartTime, @EndTime)";
                                using (var insertCmd = new SqlCommand(insertQuery, conn, trans))
                                {


                                    insertCmd.Parameters.AddWithValue("@StudentId", studentId);
                                    insertCmd.Parameters.AddWithValue("@Day", day);
                                    insertCmd.Parameters.AddWithValue("@StartTime", startTime.TimeOfDay);
                                    insertCmd.Parameters.AddWithValue("@EndTime", endTime.TimeOfDay);
                                    insertCmd.ExecuteNonQuery();


                                }


                            }


                        }


                        trans.Commit();
                    }


                    catch (Exception ex)
                    {


                        trans.Rollback();
                        Console.WriteLine("DATABASE ERROR in UpdateStudentSchedule: " + ex.Message);
                        success = false;


                    }


                }

                conn.Close();


            }


            return success;


        }



    }
}