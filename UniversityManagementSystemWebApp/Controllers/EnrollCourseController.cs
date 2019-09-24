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
    public class EnrollCourseController : Controller
    {

        EnrollGateway aEnrollGateway= new EnrollGateway();
        EnrollManager aEnrollManager= new EnrollManager();
        //
        // GET: /EnrollCourse/
        public ActionResult Enroll()
        {
            RegistratiuonNoBind();
            return View();
        }


        [HttpPost]
        public ActionResult Enroll(EnrollCourse aEnrollCourse)
        {
            RegistratiuonNoBind();
            ViewBag.Message = aEnrollManager.Save(aEnrollCourse);
            return View();
        }


        public void RegistratiuonNoBind()
        {

            DataSet ds = aEnrollManager.GetRegistrationNo();

            List<SelectListItem> registrationNoItems = new List<SelectListItem>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                registrationNoItems.Add(new SelectListItem { Text = dr["RegistrationNo"].ToString(), Value = dr["Id"].ToString() });

            }

            ViewBag.RegistrationNo = registrationNoItems;

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

        public JsonResult CourseBind(int studentId)
        {
            DataSet ds = aEnrollManager.GetAllCourse(studentId);

            List<SelectListItem> courseList = new List<SelectListItem>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                courseList.Add(new SelectListItem { Text = dr["Name"].ToString(), Value = dr["Id"].ToString() });
            }

            return Json(courseList, JsonRequestBehavior.AllowGet);

        }
	}
}