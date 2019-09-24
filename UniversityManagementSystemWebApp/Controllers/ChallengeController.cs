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
    public class ChallengeController : Controller
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



        //[AcceptVerbs(HttpVerbs.Get)]
        //public JsonResult CustomData(int id)
        //{
        //    // here I get the data from the database in result
        //    var result = WebGridBinbd(id).ToList();

        //    //now I create the new webgrid ,also i will pass result as it parameter
        //    var grid = new WebGrid(result);

        //    //now i create the columns of the grid ....
        //    var htmlString = grid.GetHtml
        //    (tableStyle: "paramTable", htmlAttributes: new { id = "grid" },
        //                                              columns: grid.Columns(
        //                                                  grid.Column("CourseCode", "Code"),
        //                                                  grid.Column("CourseName", "Name"),
        //                                                  grid.Column("Result","Grade")
        //                                                  ));

        //    // while returning i am passing this grid as htmlstring...
        //    return Json(new
        //    {
        //        Data = htmlString.ToHtmlString()
        //    }
        //        , JsonRequestBehavior.AllowGet);
        //} 

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
            // RETURN DETAILS TO THE PARENT VIEW.
            //return View("ViewResult", resultSaves);


            return Json(new
            {
                list = resultSaves
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult WebGridBind(int studentId)
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
            return Json(new
            {
                list = resultSaves
            }, JsonRequestBehavior.AllowGet);
            //return Json(resultSaves ,JsonRequestBehavior.AllowGet);
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