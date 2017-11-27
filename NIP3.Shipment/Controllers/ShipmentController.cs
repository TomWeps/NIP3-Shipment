using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NIP3.Shipment.Dto;
using Swashbuckle.Swagger.Annotations;
using NIP3.Shipment.Domain;

namespace NIP3.Shipment.Controllers
{
    [RoutePrefix("api/v1/shipment")]
    public class ShipmentController : ApiController
    {
        private readonly IShipmentRequestValidator shipmentRequestValidator;
        private readonly IShipmentDispatcher shipmentDispatcher;

        public ShipmentController(
            IShipmentRequestValidator shipmentRequestValidator,
            IShipmentDispatcher shipmentDispatcher
            )
        {
            this.shipmentRequestValidator = shipmentRequestValidator;
            this.shipmentDispatcher = shipmentDispatcher;
        }

        [HttpGet]
        [Route("{confirmationNumber}", Name = "RouteGetShipment")]
        [SwaggerResponse(HttpStatusCode.OK, type: typeof(ShipmentResponse))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult GetShipment(string confirmationNumber)
        {
            var shipmentNote = shipmentDispatcher.Get(confirmationNumber);
            if(shipmentNote == null)
            {
                return NotFound();
            }

            var content = Map(shipmentNote);
            return Ok(content);
        }


        [HttpPost]
        [Route("", Name = "RouteOrderShipment")]
        [SwaggerResponse(HttpStatusCode.Created, type: typeof(ShipmentResponse))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public IHttpActionResult OrdedrShipment(ShipmentRequest shipmentRequest)
        {
            if(!shipmentRequestValidator.IsValid(shipmentRequest))
            {
                return BadRequest();
            }

            var shipmentNote = shipmentDispatcher.Add(shipmentRequest);

            string routeName = "RouteGetShipment";
            object routeValues = new {confirmationNumber = shipmentNote.ConfirmationNumber};
            var content = Map(shipmentNote);
            
            return CreatedAtRoute(routeName, routeValues, content);
        }

        private ShipmentResponse Map(ShipmentNote shipmentNote)
        {
            var response = new ShipmentResponse
            {
                IsDelivered = shipmentNote.IsDelivered,
                ConfirmationNumber = shipmentNote.ConfirmationNumber
            };

            return response;
        }
    }
}
