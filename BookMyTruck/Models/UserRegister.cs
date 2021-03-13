using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookMyTruck.Models
{
    public class UserRegister
    {

        [DataType(DataType.PhoneNumber,ErrorMessage ="Invalid Mobile Number")]
        public string Mobile { get; set; }

        [Display (Name = "Name")]
        public string DisplayName { get; set; }


        [Display (Name ="Are you Looking for ?")]
        public string UserRole { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}