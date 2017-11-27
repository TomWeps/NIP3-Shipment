using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NIP3.Shipment.Dto;

namespace NIP3.Shipment.Domain
{
    public class ShipmentRequestValidator : IShipmentRequestValidator
    {
        public bool IsValid(ShipmentRequest shipmentRequest)
        {
            return shipmentRequest.CountryCode == "PL";
        }
    }
}