using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Controllers
{
    public class ViewAllocateRoomController : Controller
    {
        //
        // GET: /ViewAllocateRoom/

        private SqlConnection connection =
           new SqlConnection(WebConfigurationManager.ConnectionStrings["UMSConnection"].ConnectionString);

        private List<AllocateClassRoom> allocateDetailes = new List<AllocateClassRoom>();
        public ActionResult AllocateWebGridView()
        {
            Department();
            return View(allocateDetailes);
        }

        [HttpPost]
        public ActionResult GetAllocateDetails()
        {
            Department();

            string query = "SELECT * FROM Course WHERE DepartmentId = '" + Request["selectedDepartment"] + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            // POPULATE THE LIST WITH BIRD DETAILS.
            while (reader.Read())
            {
                if (!DBNull.Value.Equals(reader["IsAllocated"]))
                {
                    allocateDetailes.Add(new AllocateClassRoom
                    {

                        CourseName = reader["Name"].ToString(),
                        CourseCode = reader["Code"].ToString(),
                        CourseId = int.Parse(reader["Id"].ToString()),

                        Details = reader["Id"].ToString()


                    });
                }
                else
                {

                    allocateDetailes.Add(new AllocateClassRoom
                    {
                        CourseName = reader["Name"].ToString(),
                        CourseCode= reader["Code"].ToString(),
                        CourseId = int.Parse(reader["Id"].ToString()),

                        Details = "Not Scheduled Yet"

                    });

                }
            }

            connection.Close();


            foreach (var details in allocateDetailes)
            {
                if (details.Details == "Not Scheduled Yet")
                {
                    details.Details = "Not Scheduled Yet";
                }
                else
                {
                    details.Details = GetRoomAndScheduled(details.CourseId);

                }
            }
            // RETURN DETAILS TO THE PARENT VIEW.
            return View("AllocateWebGridView", allocateDetailes);
        }

        private string scheduleDetails;
        private string GetRoomAndScheduled(int courseId)
        {
            string query = "SELECT t.CourseId, Code=(select Code from Course where Id=t.CourseId),Name=(select name from Course where Id=t.CourseId), Details = STUFF((SELECT ' R. No : ' + s.RoomNo +',  '+ s.Day +',  ' + s.FromTime+' -  '+s.EndTime+ ' ;' FROM AllocateClassRoom s WHERE s.CourseId = t.CourseId FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,0,'') FROM AllocateClassRoom t where t.CourseId='" + courseId + "' GROUP BY t.CourseId";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                scheduleDetails = reader["Details"].ToString();
            }

            connection.Close();
            reader.Close();
            return scheduleDetails;
        }
       

        void Department()
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

        //private string query =
        //    "SELECT t.CourseId,Code=(select Code from Course " +
        //    "where Id=t.CourseId),Name=(select name from Course where Id=t.CourseId)," +
        //    "Details = STUFF((SELECT ' R. No : ' + s.RoomNo +',  '+ s.Day +',  ' + s.FromTime+' -  '+s.EndTime+ ' ;' " +
        //    "FROM AllocateClassRoom s WHERE s.CourseId = t.CourseId FOR XML PATH(''), " +
        //    "TYPE).value('.', 'NVARCHAR(MAX)'),1,0,'') FROM AllocateClassRoom t GROUP BY t.CourseId";
    }
}