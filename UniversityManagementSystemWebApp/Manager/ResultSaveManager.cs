using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using UniversityManagementSystemWebApp.Gateway;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Manager
{
    public class ResultSaveManager
    {
        ResultSaveGateway aResultSaveGateway = new ResultSaveGateway();
        public List<Grade> GetAllGrades()
        {
            return aResultSaveGateway.GetAllGrades();
        }


        public DataSet GetAllCourse(int studentId)
        {
            return aResultSaveGateway.GetAllCourse(studentId);
        }

        public string Save(ResultSave aResultSave)
        {
            if (aResultSaveGateway.IsCourseResultExists(aResultSave))
            {
                return "Result Allready Saved ";
            }
            else
            {
                int rowAffected = aResultSaveGateway.Save(aResultSave);
                if (rowAffected > 0)
                {
                    return "Save Successfull";
                }
                else
                {
                    return "Save Failed";
                }
            }
            
        }
    }
}