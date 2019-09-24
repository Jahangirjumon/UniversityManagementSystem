using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemWebApp.Gateway;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Manager
{
    public class StudentManager
    {
        StudentGateway aStudentGateway = new StudentGateway();
        public string Save(Student aStudent)
        {
            if (aStudentGateway.IsEmailExists(aStudent))
            {
                return "Student Email already exists";
            }
            int rowAffected = aStudentGateway.Save(aStudent);
            if (rowAffected > 0)
            {
                return "Save successfully";
            }
            return "Save failed";
        }

        public Student LastStudentRegistration()
        {
            return aStudentGateway.LastStudentRegistration();
        }
    }
}