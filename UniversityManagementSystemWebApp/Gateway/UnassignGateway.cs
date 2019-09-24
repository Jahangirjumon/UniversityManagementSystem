using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace UniversityManagementSystemWebApp.Gateway
{
    public class UnassignGateway
    {
        string connectionString =
               WebConfigurationManager.ConnectionStrings["UMSConnection"].ConnectionString;

        internal int UnassignAllCourse()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "Drop table NewCourse ; select * into NewCourse from Course; Update Course set AssignTo=@value;UPDATE Teacher SET RemainingCredit = CreditToBeTaken";
            SqlCommand command = new SqlCommand(query,connection);
            command.Parameters.Clear();
            command.Parameters.Add("value", SqlDbType.VarChar);
            command.Parameters["value"].Value = DBNull.Value;
            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            return rowAffected;
        }

        internal int UnallocateAllClassroom()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "Drop table NewAllocateClassRoom ; select * into NewAllocateClassRoom from AllocateClassRoom; Update Course set IsAllocated=@value;truncate table AllocateClassRoom;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Clear();
            command.Parameters.Add("value", SqlDbType.VarChar);
            command.Parameters["value"].Value = DBNull.Value;
            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            return rowAffected;
        }
    }
}