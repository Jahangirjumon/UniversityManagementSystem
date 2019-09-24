using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemWebApp.Manager;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Controllers
{
    public class ResultSaveController : Controller
    {
        EnrollManager aEnrollManager = new EnrollManager();
        ResultSaveManager aResultSaveManager= new ResultSaveManager();
        //
        // GET: /ResultSave/
        public ActionResult ResultSave()
        {
            RegistratiuonNoBind();
            ViewBag.Grades = aResultSaveManager.GetAllGrades();
            return View();
        }

        [HttpPost]

        public ActionResult ResultSave(ResultSave aResultSave)
        {
            RegistratiuonNoBind();
            ViewBag.Grades = aResultSaveManager.GetAllGrades();
            ViewBag.Message = aResultSaveManager.Save(aResultSave);
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
            DataSet ds = aResultSaveManager.GetAllCourse(studentId);

            List<SelectListItem> courseList = new List<SelectListItem>();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                courseList.Add(new SelectListItem { Text = dr["Name"].ToString(), Value = dr["Id"].ToString() });
            }

            return Json(courseList, JsonRequestBehavior.AllowGet);

        }

	}
}