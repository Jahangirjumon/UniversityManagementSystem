using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemWebApp.Models
{
    public class EnrollCourse
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public string RegistartionNo { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string DateEntered { get; set; }
        
        public string DepartmentName { get; set; }

        public string Result { get; set; }

        public string CourseName { get; set; }

        public string CourseCode { get; set; }




    }
}