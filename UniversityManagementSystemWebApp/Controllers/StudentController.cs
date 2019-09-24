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
    public class StudentController : Controller
    {
        DepartmentManager adeDepartmentManager = new DepartmentManager();
        StudentManager aStudentManager = new StudentManager();
        //
        // GET: /Student/
        //public ActionResult Register()
        //{
        //    ViewBag.Departments = adeDepartmentManager.GetAllDepartments();
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Register(Student aStudent)
        //{
        //    ViewBag.Departments = adeDepartmentManager.GetAllDepartments();
            
        //    return View();
        //}

        public ActionResult Register()
        {
            ViewBag.Departments = adeDepartmentManager.GetAllDepartments();
            return View();
        }

        [HttpPost]
        public ActionResult Register(Student aStudent)
        {
            ViewBag.Departments = adeDepartmentManager.GetAllDepartments();
            ViewBag.Messages = aStudentManager.Save(aStudent);
            ViewBag.LastRegistrationStudent = aStudentManager.LastStudentRegistration();

            if (ViewBag.Messages == " Email already exists"||ViewBag.Messages==null)
            {
                return View(); 
               }// return Redirect("/Student/ViewRegistrationInfo");
            return Redirect("/Student/ViewRegistrationInfo");
             
             
            
        }

        public ActionResult ViewRegistrationInfo()
        {
            ViewBag.LastRegistrationStudent=aStudentManager.LastStudentRegistration();
            return View();
        }
	}
}