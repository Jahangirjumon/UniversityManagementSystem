using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemWebApp.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int DesignationId { get; set; }
        public double CreditToBeTaken { get; set; }
        public double RemainingCredit { get; set; }
        public int DepartmentId { get; set; }
        public string ContactNo { get; set; }
    }
}