using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemWebApp.Gateway;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Manager
{
    public class CourseManager
    {
        CourseGateway aCourseGateway =new CourseGateway();
        public string Save(Course course)
        {
            if (aCourseGateway.IsCourseCodeExists(course))
            {
                return "Course Code already exists";
            }

            else if (aCourseGateway.IsCourseNameExists(course))
            {
                return "Course Name already exists";
            }
            else
            {
                int rowAffected = aCourseGateway.Save(course);
                if (rowAffected > 0)
                {
                    return "Save successfully";
                }
                return "Save failed";
            }
        }
    }
}