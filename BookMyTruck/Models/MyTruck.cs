using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookMyTruck.Models
{
    public class MyTruck
    {

        [Required (ErrorMessage ="Please Enter truck Number")]
        [MaxLength(10,ErrorMessage ="Please Enter Valid Truck  Number")]
        [MinLength(10, ErrorMessage = "Please Enter Valid Truck  Number")]
        [Display (Name ="Truck Number")]
        public string TruckNumber { get; set; }

        [Required(ErrorMessage = "Please Select truck Type")]
        [Display(Name = "Truck Type")]
        public string TruckType { get; set; }

        [Display(Name = "Manager Id")]
        public string ManagerId { get; set; }

        [Required(ErrorMessage = "Please Driver Name")]
        [Display(Name = "Driver Name")]
        public string DriverName { get; set; }

        [Required(ErrorMessage = "Please Driver Driving Licence")]
        [MaxLength(15, ErrorMessage = "Invalid Driving Licence Number")]
        [MinLength(15, ErrorMessage = "Invalid Driving Licence Number")]
        [Display(Name = "Driver Driving Licence")]
        public string DriverLicenceNumber { get; set; }

        [Required(ErrorMessage = "Please Select Pick up City")]
        [Display(Name = "Pick up City")]
        public string PickCity { get; set; }

        [Required(ErrorMessage = "Please Select Drop City")]
        [Display(Name = "Drop City")]
        public string DropCity { get; set; }

        [Required(ErrorMessage = "Please enter Maximum Capacity ")]
        [Display(Name = "Maximum Capacity in Ton's")]
        public double MaxCapacity { get; set; }

        [Required(ErrorMessage = "Please Enter Cost")]
        [Display(Name = "Minimum Cost")]
        public double CostPerKM { get; set; }

        [Display(Name = "Booking Status")]
        public bool BookedStatus { get; set; }

        [Display (Name ="Add or Remove")]
        public bool TruckStatus { get; set; }
    }
}