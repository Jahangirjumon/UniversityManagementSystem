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
    public class ResultSaveGateway
    {
        SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["UMSConnection"].ConnectionString);


        public List<Grade> GetAllGrades()
        {
            string query = "SELECT * FROM Grades";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            List<Grade> grades = new List<Grade>();

            while (reader.Read())
            {
                Grade aGrade = new Grade
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString()
                };
                grades.Add(aGrade);
            }

            reader.Close();
            connection.Close();

            return grades;
        }


        public DataSet GetAllCourse(int studentId)
        {

            //string query = " SELECT * FROM Course WHERE Course.Id IN ( SELECT CourseId FROM EnrollCourse WHERE StudentId = '1')";
            SqlCommand com = new SqlCommand("SELECT * FROM Course WHERE Course.Id IN ( SELECT CourseId FROM EnrollCourse WHERE StudentId =@id)", connection);

          com.Parameters.AddWithValue("@id", studentId);

          connection.Open();
          SqlDataAdapter da = new SqlDataAdapter(com);

          DataSet ds = new DataSet();

          da.Fill(ds);
          connection.Close();
          return ds;
        }

        public int Save(ResultSave aResultSave)
        {
            string query = "INSERT INTO Result VALUES(@id,@name,@email,@departmentName,@courseId,@grade);Update EnrollCourse set Result = @result where StudentId=@student and CourseId =@course; ";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Clear();

            command.Parameters.Add("id", SqlDbType.Int);
            command.Parameters["id"].Value = aResultSave.RegistrationNo;

            command.Parameters.Add("name", SqlDbType.VarChar);
            command.Parameters["name"].Value = aResultSave.StudentName;

            command.Parameters.Add("email", SqlDbType.VarChar);
            command.Parameters["email"].Value = aResultSave.Email;

            command.Parameters.Add("departmentName", SqlDbType.VarChar);
            command.Parameters["departmentName"].Value = aResultSave.DepartmentName;

            command.Parameters.Add("courseId", SqlDbType.Int);
            command.Parameters["courseId"].Value = aResultSave.CourseId;

            command.Parameters.Add("grade", SqlDbType.VarChar);
            command.Parameters["grade"].Value = aResultSave.Grade;

            command.Parameters.Add("result", SqlDbType.VarChar);
            command.Parameters["result"].Value = aResultSave.Grade;

            command.Parameters.Add("student", SqlDbType.Int);
            command.Parameters["student"].Value = aResultSave.RegistrationNo;

            command.Parameters.Add("course", SqlDbType.Int);
            command.Parameters["course"].Value = aResultSave.CourseId;

            connection.Open();

            int rowAffected = command.ExecuteNonQuery();

            connection.Close();

            return rowAffected;
        }

        internal bool IsCourseResultExists(ResultSave aResultSave)
        {

            string query = "Select * from Result where StudentId = @id and CourseId = @courseId";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Clear();

            command.Parameters.Add("id", SqlDbType.Int);
            command.Parameters["id"].Value = aResultSave.RegistrationNo;

            command.Parameters.Add("courseId", SqlDbType.Int);
            command.Parameters["courseId"].Value = aResultSave.CourseId;
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