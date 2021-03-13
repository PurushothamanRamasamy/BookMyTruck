using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookMyTruck.Models
{
    public class Login
    {

        [Required]
        [RegularExpression("^[6-9]{1}[0-9]{9}$",ErrorMessage ="Invalid Mobile Number")]
        [Display(Name = "Mobile Number")]
        public string Mobile { get; set; }

        [Required ]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}