using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Newtonsoft.Json;
using NIP3.Shipment.Test.Helpers.Stubs;
using NIP3.Shipment.Domain;
using NIP3.Shipment.Dto;

namespace NIP3.Shipment.Test.Helpers
{
    public class ShipmentServiceProviderStateMiddleware
    {
        private const string ConsumerName = "OrdersService";
        public const string ProviderStateRelativeUri = "/pact-provider-state";

        private readonly Func<IDictionary<string, object>, Task> next;
        private readonly IDictionary<string, Action> providerStates;
        private readonly ShipmentRequestValidatorStub shipmentRequestValidator;
        private readonly ShipmentDispatcher shipmentDispatcher;

        public ShipmentServiceProviderStateMiddleware(
            Func<IDictionary<string, object>, Task> next, 
            ShipmentRequestValidatorStub shipmentRequestValidator,
            ShipmentDispatcher shipmentDispatcher
            )
        {
            this.next = next;
            this.shipmentRequestValidator = shipmentRequestValidator;
            this.shipmentDispatcher = shipmentDispatcher;

            providerStates = new Dictionary<string, Action>
            {
                {
                    "valid shipment data",
                    ()=> { return; } // TODO: This method does nothing, change it to the correct one
                },
                {
                    "invalid shipment data",
                    ()=> { return; } // TODO: This method does nothing, change it to the correct one
                },
                {
                    "there is a shipment number 'XX-636446338738089968' not delivered",
                    ()=> { return; } // TODO: This method does nothing, change it to the correct one
                }                
            };
        }

        private void SetShipmentValidatorToReturnAlwaysValidResult()
        {
            shipmentRequestValidator.ValidationResult = true;
        }

        private void SetShipmentValidatorToReturnAlwaysInvalidResult()
        {
            shipmentRequestValidator.ValidationResult = false;
        }

        private void AddShipmentNumberXX63644633873808996()
        {            
            var shipmentNote = new ShipmentNote
            {
                ConfirmationNumber = "XX-636446338738089968",
                IsDelivered = false,
                Created = DateTime.Now,
                Request = new ShipmentRequest
                {
                    Name = "Jan Kowalski",
                    Address = "Akademicka 16",
                    City = "Gliwice",
                    PostalCode = "44-100",
                    CountryCode = "PL",
                    Email = "jan.kowalski@polsl.pl",
                    PhoneNumber = "32 237 13 10"
                }
            };
            shipmentDispatcher.Add(shipmentNote);
        }


        public async Task Invoke(IDictionary<string, object> environment)
        {
            IOwinContext context = new OwinContext(environment);

            if (context.Request.Path.Value == ProviderStateRelativeUri)
            {
                context.Response.StatusCode = (int) HttpStatusCode.OK;

                if (context.Request.Method == HttpMethod.Post.ToString() &&
                    context.Request.Body != null)
                {
                    string jsonRequestBody;
                    using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
                    {
                        jsonRequestBody = reader.ReadToEnd();
                    }

                    var providerState = JsonConvert.DeserializeObject<ShipmentServiceProviderState>(jsonRequestBody);
                    
                    if (providerState != null
                        && !string.IsNullOrEmpty(providerState.State)
                        && providerState.Consumer == ConsumerName
                        )
                    {
                        providerStates[providerState.State].Invoke();
                    }

                    await context.Response.WriteAsync(string.Empty);
                }

            }
            else
            {
                await next.Invoke(environment);
            }
        }
    }
}
