using NIP3.Shipment.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NIP3.Shipment.Domain
{
    public class ShipmentNote
    {
        public string ConfirmationNumber { get; set; }
        public ShipmentRequest Request { get; set; }
        public DateTime Created { get; set; }

        public bool IsDelivered { get; set; }
        
        public ShipmentNote(ShipmentRequest request)
        {
            var now = DateTime.Now;

            this.Request = request;
            this.ConfirmationNumber = $"XX-{now.Ticks}";
            this.Created = now;
            this.IsDelivered = false;
        }

        public ShipmentNote()
        {
            
        }
    }
}