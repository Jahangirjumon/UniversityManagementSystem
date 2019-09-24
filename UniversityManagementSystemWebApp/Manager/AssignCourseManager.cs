using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemWebApp.Gateway;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Manager
{
    public class AssignCourseManager
    {
        CourseAssignGateway assignGateway= new CourseAssignGateway();
        public string Save(AssignCourse assignCourse)
        {
            if (assignGateway.IsCourseAssignExists(assignCourse))
            {
                return "Already Assigned";
            }

            else
            {
                int rowAffected = assignGateway.Save(assignCourse);
                if (rowAffected > 0)
                {
                    return "Course Assigned successfully";
                }
                return "Failed";
                
            }
            
        }


        internal Course CourseNameAndCredit(int courseId)
        {
            return assignGateway.CourseNameAndCredit(courseId);
        }

        internal Teacher AllCreditInfo(int teacherId)
        {
            return assignGateway.AllCreditInfo(teacherId);
        }

    }
}