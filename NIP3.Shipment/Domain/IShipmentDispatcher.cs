using System;
using NIP3.Shipment.Dto;

namespace NIP3.Shipment.Domain
{
    public interface IShipmentDispatcher
    {
        ShipmentNote Add(ShipmentRequest shipmentRequest);
        ShipmentNote Get(string confirmationNumber);
    }
}