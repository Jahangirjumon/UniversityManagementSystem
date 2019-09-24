using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemWebApp.Models
{
    public class AssignCourse
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "Department Name is required")]
        public double CreditToBetaken { get; set; }
        public double RemainingCredit { get; set; }
        public double Credit { get; set; }



    }
}