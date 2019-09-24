using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using UniversityManagementSystemWebApp.Controllers;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Gateway
{
    public class CourseStatisticsGateway
    {
        private SqlConnection connection =
        new SqlConnection(WebConfigurationManager.ConnectionStrings["UMSConnection"].ConnectionString);

        private List<CourseDetails> courseDetailses = new List<CourseDetails>();

        //private List<SelectListItem> GetDepartment()
        //{
        //    List<SelectListItem> departments = new List<SelectListItem>();

        //    string query = "SELECT * FROM Department";
        //    SqlCommand command = new SqlCommand(query, connection);
        //    connection.Open();
        //    SqlDataReader reader = command.ExecuteReader();

        //    departments.Insert(0, new SelectListItem()
        //    {
        //        Value = "0",
        //        Text = "-- Select Department --"
        //    });
        //    // POPULATE THE LIST WITH DATA.
        //    while (reader.Read())
        //    {
        //        departments.Add(new SelectListItem
        //        {

        //            Text = reader["Name"].ToString(),
        //            Value = reader["Id"].ToString(),
        //            Selected = Request["selectedDepartment"] == reader["Id"].ToString() ? true : false
        //        });
        //    }

        //    connection.Close();

        //    return departments;
          
        //}


    }
}