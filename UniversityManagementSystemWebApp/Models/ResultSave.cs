using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemWebApp.Models
{
    public class ResultSave
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int RegistrationNo { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string Grade { get; set; }
        //public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}