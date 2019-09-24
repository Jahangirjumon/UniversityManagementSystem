using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Web.Mvc;
using UniversityManagementSystemWebApp.Manager;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Controllers
{
    public class ViewResultController : Controller
    {
       
        private SqlConnection connection =
           new SqlConnection(WebConfigurationManager.ConnectionStrings["UMSConnection"].ConnectionString);
        List<EnrollCourse> resultSaves = new List<EnrollCourse>(); 
        EnrollManager aEnrollManager = new EnrollManager();
        //
        // GET: /ViewResult/
        public ActionResult ViewResult()
        {
            GetRegistartionNo();
            return View(resultSaves);
        }
        private void GetRegistartionNo()
        {
            List<SelectListItem> registartionNo = new List<SelectListItem>();

            string query = "select EnrollCourse.* from (select EnrollCourse.*, row_number() over (partition by StudentId order by StudentId) as seqnum from EnrollCourse) EnrollCourse where seqnum = 1";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            registartionNo.Insert(0, new SelectListItem()
            {
                Value = "0",
                Text = "--Select Registration No--"
            });
            // POPULATE THE LIST WITH DATA.
            while (reader.Read())
            {
                registartionNo.Add(new SelectListItem
                {

                    Text = reader["RegistartionNo"].ToString(),
                    Value = reader["StudentId"].ToString(),
                    Selected = Request["selectedRegistartionNo"] == reader["StudentId"].ToString() ? true : false
                });
            }

            // ADD LIST TO A ViewBag. WILL USE THIS LIST TO POPULATE THE DROPDOWNLIST IN OUR VIEW.
            ViewBag.selectedRegistartionNo = registartionNo;

            connection.Close();
        }

        [HttpPost()]
        public ActionResult GetResultDetails()
        {
            GetRegistartionNo();

            string query = "SELECT * FROM EnrollCourse WHERE StudentId = '" + Request["selectedRegistartionNo"] + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EnrollCourse resultSave = new EnrollCourse();

            // POPULATE THE LIST WITH BIRD DETAILS.
            while (reader.Read())
            {
                if (!DBNull.Value.Equals(reader["Result"]))
                {
                    resultSaves.Add(new EnrollCourse
                    {
                        CourseName =reader["CourseId"].ToString(),
                        CourseCode = reader["CourseId"].ToString(),
                        Result = reader["Result"].ToString(),

                        StudentId = int.Parse(reader["StudentId"].ToString())


                    });
                }
                else
                {

                    resultSaves.Add(new EnrollCourse
                    {
                        CourseName =reader["CourseId"].ToString(),
                        CourseCode = reader["CourseId"].ToString(),
                        Result = "NOT PUBLISHED",

                        StudentId = int.Parse(reader["StudentId"].ToString())

                    });

                }
            }

            connection.Close();


            foreach (var details in resultSaves)
            {
                details.CourseName = GetCourseName(int.Parse(details.CourseName));
                details.CourseCode = GetCourseCode(int.Parse(details.CourseCode));
                if (details.Result == "NOT PUBLISHED")
                {
                    details.Result = "NOT PUBLISHED";
                }
                else
                {
                    //details.Result = GetTeacherName(int.Parse(details.AssignTo));

                }
            }
            // RETURN DETAILS TO THE PARENT VIEW.
           return View("ViewResult", resultSaves);


            //return Json(new
            //{
            //    list = resultSaves
            //}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult WebGridBinbd(int studentId)
        {
            string query = "SELECT * FROM EnrollCourse WHERE StudentId = '" + studentId + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            EnrollCourse resultSave = new EnrollCourse();

            // POPULATE THE LIST WITH BIRD DETAILS.
            while (reader.Read())
            {
                if (!DBNull.Value.Equals(reader["Result"]))
                {
                    resultSaves.Add(new EnrollCourse
                    {
                        CourseName = reader["CourseId"].ToString(),
                        CourseCode = reader["CourseId"].ToString(),
                        Result = reader["Result"].ToString(),

                        StudentId = int.Parse(reader["StudentId"].ToString())


                    });
                }
                else
                {

                    resultSaves.Add(new EnrollCourse
                    {
                        CourseName = reader["CourseId"].ToString(),
                        CourseCode = reader["CourseId"].ToString(),
                        Result = "NOT PUBLISHED",

                        StudentId = int.Parse(reader["StudentId"].ToString())

                    });

                }
            }

            connection.Close();


            foreach (var details in resultSaves)
            {
                details.CourseName = GetCourseName(int.Parse(details.CourseName));
                details.CourseCode = GetCourseCode(int.Parse(details.CourseCode));
                if (details.Result == "NOT PUBLISHED")
                {
                    details.Result = "NOT PUBLISHED";
                }
                else
                {
                    //details.Result = GetTeacherName(int.Parse(details.AssignTo));

                }
            }
            

            return Json(resultSaves, JsonRequestBehavior.AllowGet);
        }
        string getCourseName, getCourseCode;
        

        private string GetCourseCode(int courseId)
        {

            string query = "SELECT * FROM Course WHERE Id = '" + courseId + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                getCourseCode = reader["Code"].ToString();
            }
            connection.Close();
            reader.Close();
            return getCourseCode;
        }

        private string GetCourseName(int courseId)
        {
            string query = "SELECT * FROM Course WHERE Id = '" + courseId + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                getCourseName = reader["Name"].ToString();
            }

            connection.Close();
            reader.Close();
            return getCourseName;
        }

        public JsonResult NameBind(int studentId)
        {

            Student student = aEnrollManager.GetNameAndEmail(studentId);

            return Json(student.Name, JsonRequestBehavior.AllowGet);

        }

        public JsonResult EmailBind(int studentId)
        {

            Student student = aEnrollManager.GetNameAndEmail(studentId);

            return Json(student.Email, JsonRequestBehavior.AllowGet);

        }

        public JsonResult DepartmentBind(int studentId)
        {

            Department dept = aEnrollManager.DepartmentName(studentId);
            return Json(dept.Name, JsonRequestBehavior.AllowGet);

        }

       

        
	}
}