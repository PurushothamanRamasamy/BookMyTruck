//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookMyTruck
{
    using System;
    using System.Collections.Generic;
    
    public partial class Truck
    {
        public string TruckNumber { get; set; }
        public string TruckType { get; set; }
        public string ManagerId { get; set; }
        public string DriverName { get; set; }
        public string DriverLicenceNumber { get; set; }
        public string PickCity { get; set; }
        public string DropCity { get; set; }
        public int MaxCapacity { get; set; }
        public double CostPerKM { get; set; }
        public bool TruckStatus { get; set; }
    
        public virtual User User { get; set; }
    }
}
