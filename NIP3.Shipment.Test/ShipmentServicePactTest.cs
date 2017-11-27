using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NIP3.Shipment.Test.Helpers;
using PactNet;
using PactNet.Infrastructure.Outputters;
using Xunit;
using Xunit.Abstractions;

namespace NIP3.Shipment.Test
{
    public class ShipmentServicePactTest
    {
        private readonly ITestOutputHelper xUnitOutput;

        public ShipmentServicePactTest(ITestOutputHelper output)
        {
            xUnitOutput = output;
        }

        [Fact]
        public void EnsureShipmentServiceHonorsPactWithOrderServiceConsumer()
        {
            string pactFileName = "ordersservice-shipmentservice.json";
            string shipmentServiceUri = TestEnvironment.ShipmentServiceUri.ToString();            

            using (ShipmentServiceTestServer.Create())
            {
                IPactVerifier pactVerifier = new PactVerifier(GetPactVerifierConfig());
                pactVerifier
                    .ProviderState($"{shipmentServiceUri.TrimEnd('/')}{ShipmentServiceProviderStateMiddleware.ProviderStateRelativeUri}")
                    .ServiceProvider("ShipmentService", shipmentServiceUri )
                    .HonoursPactWith("OrdersService")                    
                    .PactUri($"..\\..\\Pacts\\{pactFileName}")
                    .Verify()
                    ;
            }
        }

        private PactVerifierConfig GetPactVerifierConfig()
        {
            var pactConfig = new PactVerifierConfig()
            {
                // NOTE: By default PACT is using a Console Output, 
                // however xUnit 2 does not capture the console output, 
                // so a custom outputter is required.
                Outputters = new List<IOutput>
                {
                    new XUnitOutput(xUnitOutput)
                },
                Verbose = true // Output verbose verification logs to the test output.
            };
            return pactConfig;
        }
    }
}
