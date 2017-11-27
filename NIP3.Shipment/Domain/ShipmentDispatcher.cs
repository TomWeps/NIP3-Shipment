using NIP3.Shipment.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NIP3.Shipment.Dto;

namespace NIP3.Shipment.Domain
{
    public class ShipmentDispatcher : IShipmentDispatcher
    {
        private readonly Dictionary<string, ShipmentNote> dictionary;
        public ShipmentDispatcher()
        {
            dictionary = new Dictionary<string, ShipmentNote>();
        }

        public ShipmentNote Add(ShipmentRequest shipmentRequest)
        {
            var note = new ShipmentNote(shipmentRequest);            
            return Add(note);
        }

        internal ShipmentNote Add(ShipmentNote note)
        {
            dictionary.Add(note.ConfirmationNumber, note);
            return note;
        }

        public ShipmentNote Get(string confirmationNumber)
        {
            ShipmentNote value;
            dictionary.TryGetValue(confirmationNumber, out value);
            return value;
        }
    }
}