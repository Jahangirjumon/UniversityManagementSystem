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
    public class TeacherGateway
    {

        string connectionString =
                WebConfigurationManager.ConnectionStrings["UMSConnection"].ConnectionString;
        public List<Designation> GetAllDesignation()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Designation";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            List<Designation> designations = new List<Designation>();

            while (reader.Read())
            {
                Designation aDesignation = new Designation
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString()
                };
                designations.Add(aDesignation);
            }
            
            reader.Close();
            connection.Close();

            return designations;
        }

        public bool IsEmailExists(Teacher aTeacher)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT Email FROM Teacher WHERE Email='" + aTeacher.Email + "'";

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

        public int Save(Teacher aTeacher)
        {
            aTeacher.RemainingCredit = aTeacher.CreditToBeTaken;
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "INSERT INTO Teacher VALUES(@name,@address,@email,@contactNo,@designationId,@departmentId,@credit,@remainingCredit)";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Clear();
            command.Parameters.Add("name", SqlDbType.VarChar);
            command.Parameters["name"].Value = aTeacher.Name;
            command.Parameters.Add("address", SqlDbType.VarChar);
            command.Parameters["address"].Value = aTeacher.Address;
            command.Parameters.Add("email", SqlDbType.VarChar);
            command.Parameters["email"].Value = aTeacher.Email;
            command.Parameters.Add("contactNo", SqlDbType.VarChar);
            command.Parameters["contactNo"].Value = aTeacher.ContactNo;
            command.Parameters.Add("departmentId", SqlDbType.Int);
            command.Parameters["departmentId"].Value = aTeacher.DepartmentId;
            command.Parameters.Add("designationId", SqlDbType.Int);
            command.Parameters["designationId"].Value = aTeacher.DesignationId;
            command.Parameters.Add("credit", SqlDbType.Decimal);
            command.Parameters["credit"].Value = aTeacher.CreditToBeTaken;
            command.Parameters.Add("remainingCredit", SqlDbType.Decimal);
            command.Parameters["remainingCredit"].Value = aTeacher.RemainingCredit;


            connection.Open();

            int rowAffected = command.ExecuteNonQuery();

            connection.Close();

            return rowAffected;
        }
    }
}