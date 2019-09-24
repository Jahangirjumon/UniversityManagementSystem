using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemWebApp.Gateway;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Manager
{
    
    public class TeacherManager
    {
        TeacherGateway aTeacherGateway=new TeacherGateway();
        public string Save(Teacher aTeacher)
        {
            if (aTeacherGateway.IsEmailExists(aTeacher))
            {
                return " Email already exists";
            }
               int rowAffected = aTeacherGateway.Save(aTeacher);
                if (rowAffected > 0)
                {
                    return "Save successfully";
                }
                return "Save failed";
        }
        public List<Designation> GetAllDesignation()
        {
            return aTeacherGateway.GetAllDesignation();
        }

    }
}