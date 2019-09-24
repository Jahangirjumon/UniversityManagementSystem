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
    public class LoginController : Controller
    {
        private SqlConnection connection =
            new SqlConnection(WebConfigurationManager.ConnectionStrings["UMSConnection"].ConnectionString);

        public ActionResult Login()
        {
            return View();
        }
        string name, password;
        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            string query = "select * from Login";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                name = reader["userName"].ToString();
                password = reader["password"].ToString();
            }

            if (admin.UserName == name && admin.Password == password)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "!wrong Username and Password";
                return View();
            }
        }


	}
}