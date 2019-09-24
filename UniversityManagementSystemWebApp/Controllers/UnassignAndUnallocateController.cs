using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemWebApp.Manager;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Controllers
{
    public class UnassignAndUnallocateController : Controller
    {
        UnassignManger unassign= new UnassignManger();
       
        [HttpGet]
        public ActionResult Unassign()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Unassign(Course aCourse)
        {
            ViewBag.Message = unassign.UnassignAllCourse();
            return View();
        }

        [HttpGet]
        public ActionResult UnAllocate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UnAllocate(AllocateClassRoom allocate)
        {
            ViewBag.Message = unassign.Unallocate();
            return View();

        }


	}
}