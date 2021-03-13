using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookMyTruck.Models
{
    public class UserMobile
    {
        [Required (ErrorMessage ="Please enter your mobile number")]
        [RegularExpression("^[6-9]{1}[0-9]{9}$", ErrorMessage = "Invalid Mobile Number")]
        [Display(Name = "Mobile Number")]
        public String Mobile { get; set; }
    }
}