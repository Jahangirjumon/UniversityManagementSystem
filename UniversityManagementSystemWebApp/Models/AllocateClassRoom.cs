using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemWebApp.Models
{
    public class AllocateClassRoom
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Department Name is required")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Course Name is required")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Room No is required")]
        public string RoomNo { get; set; }
        [Required(ErrorMessage = "Email id is required" +
                                 "Pick a Day")]
        public string Day { get; set; }
        [Required(ErrorMessage = "Pick a Begin Time")]
        public string FromTime { get; set; }
        public string FromRadioButton { get; set; }
        public string ToRadioButton { get; set; }

        [Required(ErrorMessage = "Pick a End Time")]
        public string EndTime { get; set; }

        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string Details { get; set; }
    }
}