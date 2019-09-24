using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Gateway
{
    public class CourseAssignGateway
    {
        //string connectionString =
        //      WebConfigurationManager.ConnectionStrings["UMSConnection"].ConnectionString;

        SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["UMSConnection"].ConnectionString);

        DepartmentGateway aDepartmentGateway = new DepartmentGateway();
        public DataSet GetDepartment()
        {
           
            string query = "SELECT * FROM Department";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(command);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;

        }



        //Get all State

        public DataSet GetAllTeacher(int departmentId)
        {

            SqlCommand com = new SqlCommand("Select * from Teacher where DepartmentId=@id", connection);

            com.Parameters.AddWithValue("@id", departmentId);
            connection.Open();

            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();

            da.Fill(ds);
            
            connection.Close();

            return ds;

        }

        public DataSet GetAllCourse(int departmentId)
        {

            SqlCommand com = new SqlCommand("Select * from Course where DepartmentId=@id", connection);

            com.Parameters.AddWithValue("@id", departmentId);
            connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();

            da.Fill(ds);

            connection.Close();
            return ds;

        }


        public int Save(AssignCourse assignCourse)
        {
            if (assignCourse.RemainingCredit - assignCourse.Credit < 0)
            {
                assignCourse.RemainingCredit = 0;
            }
            else
            {
                assignCourse.RemainingCredit = assignCourse.RemainingCredit - assignCourse.Credit;
   
            }
            
            string query = "Update Course set AssignTo = @teacher where Id = @courseId;Update Teacher set RemainingCredit = @remainUpdate where Id=@teacher";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Clear();

            command.Parameters.Add("courseId", SqlDbType.Int);
            command.Parameters["courseId"].Value = assignCourse.CourseId;

            command.Parameters.Add("teacher", SqlDbType.Int);
            command.Parameters["teacher"].Value = assignCourse.TeacherId;

            command.Parameters.Add("remainUpdate", SqlDbType.Int);
            command.Parameters["remainUpdate"].Value = assignCourse.RemainingCredit;




            connection.Open();

            int rowAffected = command.ExecuteNonQuery();

            connection.Close();

            return rowAffected;
        }

        internal Course CourseNameAndCredit(int courseId)
        {
            Course aCourse = new Course();
            SqlCommand com = new SqlCommand("Select * from Course where Id = @id", connection);

            com.Parameters.AddWithValue("@id",courseId);
            connection.Open();
            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                aCourse.Name = reader["Name"].ToString();
                aCourse.Credit = double.Parse(reader["Credit"].ToString());
                
            }

            reader.Close();
            connection.Close();
            return aCourse;

        }

        internal Teacher AllCreditInfo(int teacherId)
        {
            Teacher aTeacher = new Teacher();
            SqlCommand com = new SqlCommand("Select * from Teacher where Id = @id", connection);

            com.Parameters.AddWithValue("@id",teacherId);
            connection.Open();
            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                aTeacher.CreditToBeTaken = double.Parse(reader["CreditToBeTaken"].ToString());
                aTeacher.RemainingCredit = double.Parse(reader["RemainingCredit"].ToString());

            }

            reader.Close();
            connection.Close();
            return aTeacher;
        }

        public bool IsCourseAssignExists(AssignCourse assignCourse)
        {

            string query = "Select * from Course where Id = @id and AssignTo >0";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Clear();

            command.Parameters.Add("id", SqlDbType.Int);
            command.Parameters["id"].Value = assignCourse.CourseId;

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            bool hasRow = false;

            if (reader.HasRows)
            {
                hasRow = true;
            }

            reader.Close();
            connection.Close();

            return hasRow;
        }
    }
}