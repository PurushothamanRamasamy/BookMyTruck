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
    
    public partial class Request
    {
        public int RequestId { get; set; }
        public string CustomerId { get; set; }
        public string ManagerId { get; set; }
        public string PickupCity { get; set; }
        public string DropCity { get; set; }
        public System.DateTime EstimatedStartDate { get; set; }
        public int Distance { get; set; }
        public double EstimatedCost { get; set; }
        public bool RequestStatus { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
