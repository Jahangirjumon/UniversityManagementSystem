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
    public class CourseGateway
    {
        string connectionString =
                WebConfigurationManager.ConnectionStrings["UMSConnection"].ConnectionString;

        public int Save(Course course)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "INSERT INTO Course VALUES(@code,@name,@credit,@description,@departmentId,@SemesterId,@assignTo,@allocate)";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Clear();
            command.Parameters.Add("code", SqlDbType.VarChar);
            command.Parameters["code"].Value = course.Code;
            command.Parameters.Add("name", SqlDbType.VarChar);
            command.Parameters["name"].Value = course.Name;
            command.Parameters.Add("credit", SqlDbType.Decimal);
            command.Parameters["credit"].Value = course.Credit;
            command.Parameters.Add("description", SqlDbType.VarChar);
            command.Parameters["description"].Value = course.Description;
            command.Parameters.Add("departmentId", SqlDbType.Int);
            command.Parameters["departmentId"].Value = course.DepartmentId;
            command.Parameters.Add("SemesterId", SqlDbType.Int);
            command.Parameters["SemesterId"].Value = course.SemesterId;
            command.Parameters.Add("assignTo", SqlDbType.Int);
            command.Parameters["assignTo"].Value = DBNull.Value;

            command.Parameters.Add("allocate", SqlDbType.VarChar);
            command.Parameters["allocate"].Value = DBNull.Value;
            


            connection.Open();

            int rowAffected = command.ExecuteNonQuery();

            connection.Close();

            return rowAffected;
        }
        public bool IsCourseNameExists(Course course)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT Name FROM Course WHERE Name='" + course.Name + "'";

            SqlCommand command = new SqlCommand(query, connection);

            //command.Parameters.Clear();
            //command.Parameters.Add("@name", SqlDbType.VarChar);
            //command.Parameters["@name"].Value = aDepartment.Name;
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

        public bool IsCourseCodeExists(Course course)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM Course WHERE Code='" + course.Code + "'";

            SqlCommand command = new SqlCommand(query, connection);

            //command.Parameters.Clear();
            //command.Parameters.Add("@code", SqlDbType.VarChar);
            //command.Parameters["@code"].Value = aDepartment.Code;
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