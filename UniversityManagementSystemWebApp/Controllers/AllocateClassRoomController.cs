using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemWebApp.Gateway;
using UniversityManagementSystemWebApp.Manager;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Controllers
{
    public class AllocateClassRoomController : Controller
    {
        CourseAssignGateway assignGateway = new CourseAssignGateway();
        AllocateManager allocateManager = new AllocateManager();
        DepartmentManager aDepartmentManager= new DepartmentManager();
        //
        // GET: /AllocateClassRoom/
        public ActionResult Allocate()
        {
            DepartmentBind();
            ViewBag.Courses = allocateManager.GetAllCourse();
            ViewBag.Days = allocateManager.GetAllDay();
            ViewBag.RoomNo = allocateManager.GetAllRoom();
            //ViewBag.Departments = aDepartmentManager.GetAllDepartments();

            return View();
        }
        [HttpPost]

        public ActionResult Allocate(AllocateClassRoom allocate)
        {
            DepartmentBind();
            ViewBag.Courses = allocateManager.GetAllCourse();
            ViewBag.Days = allocateManager.GetAllDay();
            ViewBag.RoomNo = allocateManager.GetAllRoom();
            //ViewBag.Departments = aDepartmentManager.GetAllDepartments();

            ViewBag.Message = allocateManager.Save(allocate);

            return View();
        }


        public void DepartmentBind()
        {

            DataSet ds = assignGateway.GetDepartment();

            List<SelectListItem> departmentList = new List<SelectListItem>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                departmentList.Add(new SelectListItem { Text = dr["Name"].ToString(), Value = dr["Id"].ToString() });

            }

            ViewBag.DepartmentId = departmentList;

        }

        public JsonResult CourseBind(int departmentId)
        {
            string code;

            DataSet ds = assignGateway.GetAllCourse(departmentId);

            List<SelectListItem> courseList = new List<SelectListItem>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                courseList.Add(new SelectListItem { Text = dr["Name"].ToString(), Value = dr["Id"].ToString() });
                code = dr["Name"].ToString();
                // ViewBag.Course = assignGateway.CourseName(code);
            }

            //            ViewBag.Course = assignGateway.CourseName(code);

            return Json(courseList, JsonRequestBehavior.AllowGet);

        }
	}
}