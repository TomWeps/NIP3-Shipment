using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NIP3.Shipment.Dto
{    
    public class ShipmentResponse
    {        
        public string ConfirmationNumber { get; set; }     
        public bool IsDelivered { get; set; }
    }
}