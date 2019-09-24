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
    public class DepartmentGateway
    {
        string connectionString =
                WebConfigurationManager.ConnectionStrings["UMSConnection"].ConnectionString;
        
        public int Save(Department aDepartment)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "INSERT INTO Department VALUES(@code,@name)";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Clear();
            command.Parameters.Add("code", SqlDbType.VarChar);
            command.Parameters["code"].Value = aDepartment.Code;
            command.Parameters.Add("name", SqlDbType.VarChar);
            command.Parameters["name"].Value = aDepartment.Name;
            
            connection.Open();

            int rowAffected = command.ExecuteNonQuery();

            connection.Close();

            return rowAffected;
        }
        public bool IsDepartmentNameExists(Department aDepartment)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT Name FROM Department WHERE Name='"+aDepartment.Name+"'";

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

        public bool IsDepartmentCodeExists(Department aDepartment)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT Name FROM Department WHERE Code='" + aDepartment.Code + "'";

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

        public List<Department> GetAllDepartments()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Department";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            List<Department> departments = new List<Department>();

            while (reader.Read())
            {
                Department aDepartment = new Department
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString()
                };
                departments.Add(aDepartment);
            }

            reader.Close();
            connection.Close();

            return departments;
        }

        public DataSet WebGridData()
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Code,Name FROM Department";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }
                }
            }

            return ds;
        }

        public List<Department> WebGrid()
        {
            List<Department> departments = new List<Department>();
            DataSet ds = new DataSet();
            ds = WebGridData();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                departments.Add(new Department
                {
                    Code = dr["Code"].ToString(),
                    Name = dr["Name"].ToString()
                    
                });
            }
            return departments;

        }


    }
}