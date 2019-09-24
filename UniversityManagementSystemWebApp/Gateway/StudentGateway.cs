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
    public class StudentGateway
    {

      
        string connectionString =
            WebConfigurationManager.ConnectionStrings["UMSConnection"].ConnectionString;
        public bool IsEmailExists(Student asStudent)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT Email FROM Student WHERE Email='" + asStudent.Email + "'";

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

        public int Save(Student aStudent)
        {
            aStudent.YearOfStudent = aStudent.DateEntered.Substring(6, 4);
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "INSERT INTO Student VALUES(@name,@email,@contactNo,@date,@address,@departmentId,@RegNo,@yearOfStudent)";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Clear();
            command.Parameters.Add("name", SqlDbType.VarChar);
            command.Parameters["name"].Value = aStudent.Name;
            command.Parameters.Add("email", SqlDbType.VarChar);
            command.Parameters["email"].Value = aStudent.Email;
            command.Parameters.Add("contactNo", SqlDbType.VarChar);
            command.Parameters["contactNo"].Value = aStudent.ContactNo;

            command.Parameters.Add("date", SqlDbType.VarChar);
            command.Parameters["date"].Value = aStudent.DateEntered;
            
            command.Parameters.Add("address", SqlDbType.VarChar);
            command.Parameters["address"].Value = aStudent.Address;
            command.Parameters.Add("departmentId", SqlDbType.Int);
            command.Parameters["departmentId"].Value = aStudent.DepartmentId;

            command.Parameters.Add("regNo", SqlDbType.VarChar);
            command.Parameters["regNo"].Value = LastStudentId(aStudent.DepartmentId,aStudent.YearOfStudent);

            command.Parameters.Add("yearOfStudent", SqlDbType.VarChar);
            command.Parameters["yearOfStudent"].Value = aStudent.YearOfStudent;


            connection.Open();

            int rowAffected = command.ExecuteNonQuery();

            connection.Close();

            return rowAffected;
        }
        string lastRegNo;

        public string LastStudentId(int departmentId,string year)
        {
           
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT Department.Name ,Student.DateAndTime, Student.RegistrationNo FROM  Student , Department WHERE  Student.DepartmentId=Department.Id and Student.Id = (SELECT MAX(Student.Id)  FROM Student where Student.DepartmentId='" + departmentId + "' and Student.YearOfStudent ='"+year+"') and Student.DepartmentId= '" + departmentId + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            Student aStudent=new Student();
          
            if (reader.Read())
            {
                //string a="BICS SFDC"+(Convert.ToInt32(lastemployee.EmployeeID.Substring(9, lastemployee.EmployeeID.Length - 9)) + 1).ToString("D3");
            
                lastRegNo = reader["Name"].ToString() +"-"+ reader["DateAndTime"].ToString().Substring(6,4) +"-"+ (Convert.ToInt32(reader["RegistrationNo"].ToString().Substring(9,3))+1).ToString("D3");
                //aStudent.RegNo = reader["DepartmentId"] + reader["DateAndTime"].ToString()+"001 ";
            }
            else
            {
                lastRegNo = NewStudentId(departmentId) + "-" + year + "-" + "001";
            
                
            }

            //lastRegNo= aStudent.RegNo;

            reader.Close();
            connection.Close();

            return lastRegNo;



            
        }
        string departmentName;
        public string NewStudentId(int departmentId)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT Name from Department WHERE Id='"+departmentId+"'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                //string a="BICS SFDC"+(Convert.ToInt32(lastemployee.EmployeeID.Substring(9, lastemployee.EmployeeID.Length - 9)) + 1).ToString("D3");

                 departmentName = reader["Name"].ToString();
            }


            reader.Close();
            connection.Close();

            return departmentName;




        }
        List<Student>  students = new List<Student>(); 
        public Student LastStudentRegistration()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM Student WHERE Id = (SELECT MAX(Id) FROM Student)";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            Student aStudent=new Student();
            while (reader.Read())
            {
                aStudent.RegNo = reader["RegistrationNo"].ToString();
                aStudent.Name = reader["Name"].ToString();
                aStudent.Address=reader["Address"].ToString();
                aStudent.ContactNo = reader["ContactNo"].ToString();
                aStudent.Email = reader["Email"].ToString();
                aStudent.DepartmentName = reader["DepartmentId"].ToString();
            }

            reader.Close();
            connection.Close();

            aStudent.DepartmentName = GetDepartmentName(int.Parse(aStudent.DepartmentName));

            return aStudent;

        }

        private string getdepartmentName;
        public string GetDepartmentName(int departmentId)
        {

            SqlConnection connection = new SqlConnection(connectionString);


            string query = "SELECT * FROM Department WHERE Id = '" + departmentId + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                getdepartmentName = reader["Name"].ToString();
            }

            connection.Close();
            reader.Close();
            return getdepartmentName;
        }
    }
}