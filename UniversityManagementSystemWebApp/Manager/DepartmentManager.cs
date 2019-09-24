using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using UniversityManagementSystemWebApp.Gateway;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Manager
{
    public class DepartmentManager
    {
        DepartmentGateway aDepartmentGateway = new DepartmentGateway();
        public string Save(Department aDepartment)
        {
            if (aDepartmentGateway.IsDepartmentCodeExists(aDepartment))
            {
                return " Department Code already exists";
            }
            
            else if (aDepartmentGateway.IsDepartmentNameExists(aDepartment))
            {
                return " Department Name already exists";
            }
            else
            {
                int rowAffected = aDepartmentGateway.Save(aDepartment);
                if (rowAffected > 0)
                {
                    return "Save successfully";
                }
                return "Save failed";
            }
        }

        public List<Department> GetAllDepartments()
        {
            return aDepartmentGateway.GetAllDepartments();
        }



        public List<Department> WebGrid()
        {
            return aDepartmentGateway.WebGrid();
        }
    }
}