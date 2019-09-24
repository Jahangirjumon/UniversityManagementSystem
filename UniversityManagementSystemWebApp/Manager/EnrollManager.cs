using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using UniversityManagementSystemWebApp.Gateway;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Manager
{
    public class EnrollManager
    {
        EnrollGateway aEnrollGateway= new EnrollGateway();
        public DataSet GetRegistrationNo()
        {
            return aEnrollGateway.GetRegistrationNo();
        }
        public Department DepartmentName(int studentId)
        {
            return aEnrollGateway.DepartmentName(studentId);
        }

        public DataSet GetAllCourse(int studentId)
        {
            return aEnrollGateway.GetAllCourse(studentId);
        }

        public Student GetNameAndEmail(int studentId)
        {
            return aEnrollGateway.GetNameAndEmail(studentId);
        }

        public string Save(EnrollCourse aEnrollCourse)
        {
            if (aEnrollGateway.IsEnrollmentExists(aEnrollCourse))
            {
                return "One Student Enroll A Course Once only";
            }
            else
            {
                int rowAffected = aEnrollGateway.Save(aEnrollCourse);
                if (rowAffected > 0)
                {
                    return "Enroll Successfull";
                }
                else
                {
                    return "Failed";
                }   
            }
            
        }
    }
}