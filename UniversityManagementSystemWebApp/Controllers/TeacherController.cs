using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemWebApp.Manager;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Controllers
{
    public class TeacherController : Controller
    {
        TeacherManager aManager= new TeacherManager();
        DepartmentManager aDepartmentManager= new DepartmentManager();
        //
        // GET: /Teacher/
        public ActionResult SaveTeacher()
        {
            ViewBag.Designations = aManager.GetAllDesignation();
            ViewBag.Departments = aDepartmentManager.GetAllDepartments();
            return View();
        }
        [HttpPost]
        public ActionResult SaveTeacher(Teacher aTeacher)
        {
            ViewBag.Departments = aDepartmentManager.GetAllDepartments();
            ViewBag.Designations = aManager.GetAllDesignation();
            ViewBag.Message = aManager.Save(aTeacher);
            return View();
        }
	}
}