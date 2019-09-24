using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementSystemWebApp.Gateway;
using UniversityManagementSystemWebApp.Models;

namespace UniversityManagementSystemWebApp.Manager
{
    public class AllocateManager
    {
        AllocateGateway allocateGateway=new AllocateGateway();
        public List<Course> GetAllCourse()
        {
            return allocateGateway.GetAllCourse();
        }

        public List<Day> GetAllDay()
        {
            return allocateGateway.GetAllDay();
        }

        public List<RoomNo> GetAllRoom()
        {
            return allocateGateway.GetAllRoom();
        }

        public string Save(AllocateClassRoom allocate)
        {
            //if (allocate.FromRadioButton == "PM")
            //{
            //    allocate.FromTime = (Convert.ToInt32(allocate.FromTime) + 12).ToString();

            //}

            //if (allocate.ToRadioButton == "PM")
            //{
            //    allocate.EndTime = (Convert.ToInt32(allocate.EndTime) + 12).ToString();

            //}

            if (allocateGateway.CheckRoomAvailablity(allocate))
            {
                return "Room Already Allocated";
            }

            else
            {

                int rowAffected = allocateGateway.Save(allocate);
                if (rowAffected > 0)
                {
                    return "Room Allocate Successfull";
                }
                else
                {
                    return "save failed";
                }
            }
            
        }

        bool TestRange(double numberToCheck, double bottom, double top)
        {
            return (numberToCheck >= bottom && numberToCheck <= top);
        }
    }
}