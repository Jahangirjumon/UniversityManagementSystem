using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Gateway
{
    public class AllocateGateway
    {
        string connectionString =
               WebConfigurationManager.ConnectionStrings["UMSConnection"].ConnectionString;
        public List<Day> GetAllDay()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Day";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            List<Day> days = new List<Day>();

            while (reader.Read())
            {
                Day aDay = new Day
                {
                    DayId = (int)reader["DayId"],
                    DayName = reader["DayName"].ToString()
                };
                days.Add(aDay);
            }

            reader.Close();
            connection.Close();

            return days;
        }

        public List<RoomNo> GetAllRoom()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM RoomNo";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            List<RoomNo> rooms = new List<RoomNo>();

            while (reader.Read())
            {
                RoomNo aRoom = new RoomNo
                {
                    RoomId = (int)reader["RoomId"],
                    RoomName = reader["RoomName"].ToString()
                };
                rooms.Add(aRoom);
            }

            reader.Close();
            connection.Close();

            return rooms;
        }

        public List<Course> GetAllCourse()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Course";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            List<Course> courses = new List<Course>();

            while (reader.Read())
            {
                Course aCourse = new Course
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString()
                };
                courses.Add(aCourse);
            }

            reader.Close();
            connection.Close();

            return courses;
        }

        public int Save(AllocateClassRoom allocate)
        {

            allocate.FromTime = allocate.FromTime +" "+allocate.FromRadioButton;
            allocate.EndTime = allocate.EndTime +" "+ allocate.ToRadioButton;

            SqlConnection connection = new SqlConnection(connectionString);

            string query = "INSERT INTO AllocateClassRoom VALUES(@departmentId,@courseId,@roomNo,@day,@from,@to);Update Course set IsAllocated ='YES' where Id=@id ";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Clear();

            command.Parameters.Add("departmentId", SqlDbType.Int);
            command.Parameters["departmentId"].Value = allocate.DepartmentId;

            command.Parameters.Add("courseId", SqlDbType.Int);
            command.Parameters["courseId"].Value = allocate.CourseId;
            
            command.Parameters.Add("roomNo", SqlDbType.VarChar);
            command.Parameters["roomNo"].Value = allocate.RoomNo;
            
            command.Parameters.Add("day", SqlDbType.VarChar);
            command.Parameters["day"].Value = allocate.Day;

            command.Parameters.Add("from", SqlDbType.VarChar);
            command.Parameters["from"].Value = allocate.FromTime;
            
            command.Parameters.Add("to", SqlDbType.VarChar);
            command.Parameters["to"].Value = allocate.EndTime;

            command.Parameters.Add("id", SqlDbType.Int);
            command.Parameters["id"].Value = allocate.CourseId;

            connection.Open();

            int rowAffected = command.ExecuteNonQuery();

            connection.Close();

            return rowAffected;
        }

        List<RoomTimeCheck> roomTimes =new List<RoomTimeCheck>();
       
        public bool CheckRoomAvailablity(AllocateClassRoom allocateClass)
        {
            if (allocateClass.FromRadioButton == "PM" )
            {
                if (double.Parse(allocateClass.FromTime) < 12)
                {
                    allocateClass.FromTime = (Convert.ToInt32(allocateClass.FromTime) + 12).ToString();
 
                }
                
            }

            if (allocateClass.ToRadioButton == "PM")
            {
                if (double.Parse(allocateClass.EndTime) < 12)
                {
                    allocateClass.EndTime = (Convert.ToInt32(allocateClass.EndTime) + 12).ToString();

                }
                
            }
            bool result = false;
            string[] fromAndTime={"0","1"};
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT FromTime,EndTime from AllocateClassRoom WHERE RoomNo = @roomNo and Day=@day";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Clear();

            command.Parameters.Add("roomNo", SqlDbType.VarChar);
            command.Parameters["roomNo"].Value = allocateClass.RoomNo;


            command.Parameters.Add("day", SqlDbType.VarChar);
            command.Parameters["day"].Value = allocateClass.Day;

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();


            bool hasRow = false;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    roomTimes.Add(new RoomTimeCheck
                    {
                        FromTime = double.Parse(reader["FromTime"].ToString().Remove(reader["FromTime"].ToString().Length-2)),
                        ToTime = double.Parse(reader["EndTime"].ToString().Remove(reader["EndTime"].ToString().Length - 2))

                    }); 

                }

                foreach (var item in roomTimes)
                {

                    if (double.Parse(allocateClass.FromTime) > item.FromTime &&
                        double.Parse(allocateClass.FromTime) < item.ToTime ||
                        double.Parse(allocateClass.EndTime) > item.FromTime &&
                        double.Parse(allocateClass.EndTime) <= item.ToTime)
                    {
                        result = true;
                        
                    }
           
                    
                }


            }
            else
            {
                reader.Close();
                connection.Close();

                return result;
            }
            reader.Close();
            connection.Close();

            return result;
        }

    }
}