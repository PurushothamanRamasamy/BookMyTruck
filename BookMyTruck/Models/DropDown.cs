using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookMyTruck.Models
{
    public class DropDown
    {
        public List<SelectListItem> TruckTypeList= new List<SelectListItem>();
        private readonly string[] _typeoftruck = { "TATA ACE / PICKUP (1.5 TON)", " TATA 407 / EICHER 14FT (4 TON)", "EICHER 17FT (5 TON)", "EICHER 19FT (7 TON)", "20FT CONTAINER (6.5 TON)", "TATA TRUCK (10 TON)", "32FT CONTAINER (7 TON)", "32FT CONTAINER (14 TON)", "32 / 40 FEET OPEN TRAILER", "TAURUS  (18 TON)  N.A", "PARCEL SERVICE NOT AVAILABLE" };

        public List<SelectListItem> RoleList = new List<SelectListItem>();
        private readonly string[] _roles = { "Book Truck", "Add My Truck" };
        public DropDown()
        {
            foreach (var item in _roles)
            {
                RoleList.Add(new SelectListItem() { Text = item, Value = item });
            }
            foreach (var item in _typeoftruck)
            {
                TruckTypeList.Add(new SelectListItem() { Text = item, Value = item.ToString() });
            }
        }
    }
}