using NIP3.Shipment.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NIP3.Shipment.Dto;

namespace NIP3.Shipment.Test.Helpers.Stubs
{
    public class ShipmentRequestValidatorStub : IShipmentRequestValidator
    {
        public bool ValidationResult { get; set; } = true;

        public bool IsValid(ShipmentRequest shipmentRequest)
        {
            return ValidationResult;
        }
    }
}
