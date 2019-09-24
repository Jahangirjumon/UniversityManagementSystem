using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Web.Mvc;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Controllers
{
    public class CourseStatisticsController : Controller
    {
        CourseAssignController assignController =new CourseAssignController();
        //CourseAssignController assignController = new CourseAssignController();
        private SqlConnection connection =
            new SqlConnection(WebConfigurationManager.ConnectionStrings["UMSConnection"].ConnectionString);

        private List<CourseDetails> courseDetailses = new List<CourseDetails>();


        public ActionResult CourseWebGridView()
        {
            GetDepartment();
            return View(courseDetailses);
        }

        private void GetDepartment()
        {
            List<SelectListItem> departments = new List<SelectListItem>();

            string query = "SELECT * FROM Department";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            departments.Insert(0, new SelectListItem()
            {
                Value = "0",
                Text = "--Select Department--"
            });
            // POPULATE THE LIST WITH DATA.
            while (reader.Read())
            {
                departments.Add(new SelectListItem
                {

                    Text = reader["Name"].ToString(),
                    Value = reader["Id"].ToString(),
                    Selected = Request["selectedDepartment"] == reader["Id"].ToString() ? true : false
                });
            }

            // ADD LIST TO A ViewBag. WILL USE THIS LIST TO POPULATE THE DROPDOWNLIST IN OUR VIEW.
            ViewBag.selectedDepartment = departments;

            connection.Close();
        }

        [HttpPost()]
        public ActionResult GetCourseDetails()
        {
            GetDepartment();

            string query = "SELECT * FROM Course WHERE DepartmentId = '" + Request["selectedDepartment"] + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            CourseDetails aCourseDetails = new CourseDetails();

            // POPULATE THE LIST WITH BIRD DETAILS.
            while (reader.Read())
            {
                if (!DBNull.Value.Equals(reader["AssignTo"]))
                {
                    courseDetailses.Add(new CourseDetails
                    {

                        Name = reader["Name"].ToString(),
                        Code = reader["Code"].ToString(),
                        SemesterName = reader["SemesterId"].ToString(),

                        AssignTo = reader["AssignTo"].ToString()


                    });
                }
                else
                {

                    courseDetailses.Add(new CourseDetails
                    {
                        Name = reader["Name"].ToString(),
                        Code = reader["Code"].ToString(),
                        SemesterName = reader["SemesterId"].ToString(),
                        AssignTo = "Not Assigned Yet"

                    });

                }
            }

            connection.Close();


            foreach (var details in courseDetailses)
            {
                details.SemesterName = GetSemesterName(int.Parse(details.SemesterName));
                if (details.AssignTo == "Not Assigned Yet")
                {
                    details.AssignTo = "Not Assigned Yet";
                }
                else
                {
                    details.AssignTo = GetTeacherName(int.Parse(details.AssignTo));

                }
            }
            // RETURN DETAILS TO THE PARENT VIEW.
            return View("CourseWebGridView", courseDetailses);
        }
        string getTeacherName, getSemesterName;
        public string GetTeacherName(int teacherId)
        {

            string query = "SELECT * FROM Teacher WHERE Id = '" + teacherId + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                getTeacherName = reader["Name"].ToString();
            }

            connection.Close();
            reader.Close();
            return getTeacherName;
        }
        public string GetSemesterName(int semesterId)
        {

            string query = "SELECT * FROM Semester WHERE Id = '" + semesterId + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                getSemesterName = reader["Name"].ToString();
            }
            connection.Close();
            reader.Close();
            return getSemesterName;
        }

        //public ActionResult CourseWebGridView()
        //{
        //    assignController.DepartmentBind();
        //    return View();
        //}


        //public Json GetDetails(int departmentId)
        //{
            
        //}
    }

}

