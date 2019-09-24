using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemWebApp.Manager;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Controllers
{
    public class CourseController : Controller
    {
        DepartmentManager adeDepartmentManager = new DepartmentManager();
        CourseManager acCourseManager=new CourseManager();
        Department department = new Department();
        //
        // GET: /Course/
        public ActionResult Save()
        {
            ViewBag.Departments = adeDepartmentManager.GetAllDepartments();
            return View();
        }
        [HttpPost]
        public ActionResult Save(Course aCourse)
        {
            ViewBag.Departments = adeDepartmentManager.GetAllDepartments();
            ViewBag.Message = acCourseManager.Save(aCourse);
            return View();
        }
	}
}