using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagementSystemWebApp.Models
{
    public class CourseDetails
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string SemesterName { get; set; }
        public string AssignTo { get; set; }
    }
}