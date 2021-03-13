using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookMyTruck.Models
{
    public class UserRegister
    {
        [Required (ErrorMessage ="Mobile Number is Required")]
        [DataType(DataType.PhoneNumber,ErrorMessage ="Invalid Mobile Number")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [Display (Name = " Enter your name Name")]
        public string DisplayName { get; set; }

        
        public string UserRole { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [Display(Name = "Enter Password")]
        public string Password { get; set; }
    }
}