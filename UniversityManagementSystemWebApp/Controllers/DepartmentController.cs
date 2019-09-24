using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystemWebApp.Manager;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentManager adeDepartmentManager = new DepartmentManager();
        public ActionResult Save()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Save(Department aDepartment)
        {
            ViewBag.Message = adeDepartmentManager.Save(aDepartment);
            return View();
        }
        public ActionResult WebGrid()
        {
           // ViewBag.DepartmentGrid = adeDepartmentManager.WebGrid();

            return View(adeDepartmentManager.WebGrid());
        }

        
	}
}