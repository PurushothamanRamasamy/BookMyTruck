using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookMyTruck.Models
{
    public class GetCustomerPlaces
    {
        [Required (ErrorMessage ="Please enter Pick up Location")]
        [Display (Name = "Pick Up Location ")]
        public string PickUp { get; set; }
        
        [Required (ErrorMessage ="Please enter Drop Location")]
        [Display (Name ="Drop Location ")]
        public string Drop { get; set; }

        [Required (ErrorMessage ="Please choose type of truck")]
        [Display (Name ="Type of truck")]
        public string Type { get; set; }
    }
}