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
    public class EnrollGateway
    {
        SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["UMSConnection"].ConnectionString);

        public DataSet GetRegistrationNo()
        {
            string query = "SELECT * FROM Student";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(command);

            DataSet ds = new DataSet();

            da.Fill(ds);

            connection.Close();
            return ds;

        }
   
      public Student GetNameAndEmail(int studentId)
      {
          Student aStudent = new Student();
          SqlCommand com = new SqlCommand("Select * from Student where Id = @id", connection);

          com.Parameters.AddWithValue("@id", studentId);
          connection.Open();
          SqlDataReader reader = com.ExecuteReader();

          while (reader.Read())
          {
              aStudent.Name = reader["Name"].ToString();
              aStudent.Email = reader["Email"].ToString();
              aStudent.DepartmentName = reader["DepartmentId"].ToString();
          }

          reader.Close();
          connection.Close();
          return aStudent;

      }

      public Department DepartmentName(int studentId)
      {
          Department dept = new Department();
          SqlCommand com = new SqlCommand("select * from Department where Department.Id=(select DepartmentId from Student where Id= @id)", connection);

          com.Parameters.AddWithValue("@id", studentId);

          connection.Open();
          SqlDataReader reader = com.ExecuteReader();

          while (reader.Read())
          {
              dept.Name = reader["Name"].ToString();
              dept.Id = int.Parse(reader["Id"].ToString());
          }

          reader.Close();
          connection.Close();
          return dept;

      }

      public DataSet GetAllCourse(int studentId)
      {

          //string query = "Select * from Course where DepartmentId=(select DepartmentId from Student where Id= @id)";
          SqlCommand com = new SqlCommand("Select * from Course where DepartmentId=(select DepartmentId from Student where Id= @id)", connection);

          com.Parameters.AddWithValue("@id", studentId);

          connection.Open();
          SqlDataAdapter da = new SqlDataAdapter(com);

          DataSet ds = new DataSet();

          da.Fill(ds);
          connection.Close();
          return ds;
      }

      public int Save(EnrollCourse aEnrollCourse)
      {
          aEnrollCourse.StudentId = GetSTduentId(aEnrollCourse.Email);
          aEnrollCourse.RegistartionNo = GetFullRegistartionNo(aEnrollCourse.StudentId);
          string query = "INSERT INTO EnrollCourse VALUES(@id,@registartionNo,@name,@email,@departmentName,@courseId,@date,@result)";
          SqlCommand command = new SqlCommand(query, connection);

          command.Parameters.Clear();

          command.Parameters.Add("id", SqlDbType.Int);
          command.Parameters["id"].Value = aEnrollCourse.StudentId;
          
          command.Parameters.Add("name", SqlDbType.VarChar);
          command.Parameters["name"].Value = aEnrollCourse.StudentName;
          
          command.Parameters.Add("email", SqlDbType.VarChar);
          command.Parameters["email"].Value = aEnrollCourse.Email;

          command.Parameters.Add("departmentName", SqlDbType.VarChar);
          command.Parameters["departmentName"].Value = aEnrollCourse.DepartmentName;

          command.Parameters.Add("courseId", SqlDbType.Int);
          command.Parameters["courseId"].Value = aEnrollCourse.CourseId;

          command.Parameters.Add("date", SqlDbType.VarChar);
          command.Parameters["date"].Value = aEnrollCourse.DateEntered;

          command.Parameters.Add("registartionNo", SqlDbType.VarChar);
          command.Parameters["registartionNo"].Value = aEnrollCourse.RegistartionNo;

          command.Parameters.Add("result", SqlDbType.VarChar);
          command.Parameters["result"].Value = DBNull.Value;


          connection.Open();

          int rowAffected = command.ExecuteNonQuery();

          connection.Close();

          return rowAffected;
      }

        private int studentId;
      private int GetSTduentId(string email)
      {
          SqlCommand com = new SqlCommand("Select * from Student where Email = @id", connection);

          com.Parameters.Add("id", SqlDbType.VarChar);
          com.Parameters["id"].Value = email;
          connection.Open();
          SqlDataReader reader = com.ExecuteReader();

          while (reader.Read())
          {
              studentId = int.Parse(reader["Id"].ToString());

          }

          reader.Close();
          connection.Close();
          return studentId;
      }

        private string fullRegistartionNo;
      private string GetFullRegistartionNo(int studentId)
      {
          SqlCommand com = new SqlCommand("Select * from Student where Id = @id", connection);

          com.Parameters.Add("id", SqlDbType.Int);
          com.Parameters["id"].Value = studentId;
          connection.Open();
          SqlDataReader reader = com.ExecuteReader();

          while (reader.Read())
          {
              fullRegistartionNo = reader["RegistrationNo"].ToString();
              
          }

          reader.Close();
          connection.Close();
          return fullRegistartionNo;
      }

      public bool IsEnrollmentExists(EnrollCourse aEnrollCourse)
      {

          string query = "Select * from EnrollCourse where StudentId = @id and CourseId = @courseId";

          SqlCommand command = new SqlCommand(query, connection);

          command.Parameters.Clear();

          command.Parameters.Add("id", SqlDbType.Int);
          command.Parameters["id"].Value = aEnrollCourse.StudentId ;

          command.Parameters.Add("courseId", SqlDbType.Int);
          command.Parameters["courseId"].Value = aEnrollCourse.CourseId;
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