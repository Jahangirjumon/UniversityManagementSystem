using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemWebApp.Gateway;

namespace UniversityManagementSystemWebApp.Manager
{
    public class UnassignManger
    {
        UnassignGateway unassign= new UnassignGateway();

        internal string UnassignAllCourse()
        {

            int rowAffected = unassign.UnassignAllCourse();
            if (rowAffected > 0)
            {
                return "Unassign All Course Succesfully";
            }
            return "Failed ! Try Again";
        }

        internal dynamic Unallocate()
        {
            int rowAffected = unassign.UnallocateAllClassroom();
            if (rowAffected > 0)
            {
                return "Unassign All Course Succesfully";
            }
            return "Failed ! Try Again";
        }
    }
}