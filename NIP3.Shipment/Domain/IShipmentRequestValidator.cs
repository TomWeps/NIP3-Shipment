using NIP3.Shipment.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIP3.Shipment.Domain
{
    public interface IShipmentRequestValidator
    {
        bool IsValid(ShipmentRequest shipmentRequest);
    }
}
