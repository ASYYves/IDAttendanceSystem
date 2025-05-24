using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;

namespace IDSystemData
{


    public class DBData : loggingAttendanceToFile
    {


        private static readonly string connectionString =
            "Data Source=DESKTOP-B8KUMH6\\SQLEXPRESS;Initial Catalog= ID_System ;Integrated Security=True;TrustServerCertificate=True;";


        private readonly SqlConnection sqlConnection;


        public DBData()
        {


            sqlConnection = new SqlConnection(connectionString);


        }


        public void logAttendanceOfStudents(string studentId, bool clockedIn)
        {


            var query = "INSERT INTO AttendanceLogs (StudentId, ClockedIn, Timestamp) VALUES (@StudentId, @ClockedIn, @Timestamp)";
            SqlCommand commandToSQL = new SqlCommand(query, sqlConnection);
            commandToSQL.Parameters.AddWithValue("@StudentId", studentId);
            commandToSQL.Parameters.AddWithValue("@ClockedIn", clockedIn);
            commandToSQL.Parameters.AddWithValue("@Timestamp", DateTime.Now);


            sqlConnection.Open();
            commandToSQL.ExecuteNonQuery();
            sqlConnection.Close();


        }


        public recordOfAttendance GetAttendance(string studentId)
        {


            var query = "SELECT ClockedIn FROM AttendanceLogs WHERE StudentId = @StudentId";
            SqlCommand commandToSQL = new SqlCommand(query, sqlConnection);
            commandToSQL.Parameters.AddWithValue("@StudentId", studentId);


            sqlConnection.Open();
            SqlDataReader reader = commandToSQL.ExecuteReader();


            var record = new recordOfAttendance();


            while (reader.Read())
            {


                bool clockedIn = reader.GetBoolean(0);


                if (!clockedIn)
                {


                    record.Lates++;


                }


            }


            reader.Close();
            sqlConnection.Close();


            return record;


        }


        public Dictionary<string, string> LoadStudents()
        {
            var students = new Dictionary<string, string>();
            var query = "SELECT StudentId, Name FROM Students";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);

            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                students[reader.GetString(0)] = reader.GetString(1);
            }
            reader.Close();
            sqlConnection.Close();

            return students;
        }

        public Dictionary<string, string> LoadSchedules()
        {
            var schedules = new Dictionary<string, string>();
            var query = "SELECT StudentId, Schedule FROM Students";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);

            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                schedules[reader.GetString(0)] = reader.GetString(1);
            }
            reader.Close();
            sqlConnection.Close();

            return schedules;
        }
    }
}
