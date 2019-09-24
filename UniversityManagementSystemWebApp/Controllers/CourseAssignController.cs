using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemWebApp.Gateway;
using UniversityManagementSystemWebApp.Manager;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Controllers
{
    public class CourseAssignController : Controller
    {

        CourseAssignGateway assignGateway =new CourseAssignGateway();
        AssignCourseManager assignCourseManager = new AssignCourseManager();
        //
        // GET: /CourseAssign/
        public ActionResult Assign()
        {
            ViewBag.Message = null;
            DepartmentBind();
            return View();
        }

        [HttpPost]
        public ActionResult Assign(AssignCourse assignCourse)
        {
            if (assignCourse.RemainingCredit - assignCourse.Credit < 0)
            {
                ViewBag.Messages = "Credit Over Load";
            }
            DepartmentBind();
            ViewBag.Message = assignCourseManager.Save(assignCourse);
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

            ViewBag.Department = departmentList;

        }


        public JsonResult TeacherBind(int departmentId)
        {

            DataSet ds = assignGateway.GetAllTeacher(departmentId);

            List<SelectListItem> teacherList = new List<SelectListItem>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                teacherList.Add(new SelectListItem { Text = dr["Name"].ToString(), Value = dr["Id"].ToString() });

            }

            return Json(teacherList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult CourseBind(int departmentId)
        {
            DataSet ds = assignGateway.GetAllCourse(departmentId);

            List<SelectListItem> courseList = new List<SelectListItem>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                courseList.Add(new SelectListItem { Text = dr["Code"].ToString(), Value = dr["Id"].ToString() });
             
            }
            
            return Json(courseList, JsonRequestBehavior.AllowGet);
            
        }

        public JsonResult CourseNameBind(int courseId)
        {

            Course aCourse = assignCourseManager.CourseNameAndCredit(courseId);

            return Json(aCourse.Name, JsonRequestBehavior.AllowGet);

        }

        public JsonResult CourseCreditBind(int courseId)
        {

            Course aCourse = assignCourseManager.CourseNameAndCredit(courseId);

            return Json(aCourse.Credit, JsonRequestBehavior.AllowGet);

        }

        public JsonResult RemainingCreditBind(int teacherId)
        {

            Teacher aTeacher = assignCourseManager.AllCreditInfo(teacherId);

            return Json(aTeacher.RemainingCredit, JsonRequestBehavior.AllowGet);

        }

        public JsonResult CreditToBeTakenBind(int teacherId)
        {

            Teacher aTeacher = assignCourseManager.AllCreditInfo(teacherId);

            return Json(aTeacher.CreditToBeTaken, JsonRequestBehavior.AllowGet);

        }

	}
}