using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookMyTruck.Models
{
    public class Login
    {
        [Required (ErrorMessage ="Mobile Number is Required")]
        [DataType(DataType.PhoneNumber,ErrorMessage ="Invalid number")]
        [Display (Name ="Mobile Number")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}