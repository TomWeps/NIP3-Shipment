using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NIP3.Shipment.Dto
{
    public class ShipmentRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string CountryCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
    }


}