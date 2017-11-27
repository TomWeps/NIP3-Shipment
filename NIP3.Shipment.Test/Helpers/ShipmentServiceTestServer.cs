using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Owin;

namespace NIP3.Shipment.Test.Helpers
{
    public static class ShipmentServiceTestServer
    {
        public static IDisposable Create()
        {
            string serviceUri = TestEnvironment.ShipmentServiceUri.ToString();

            return WebApp.Start<ShipmentServiceTestSartup>(serviceUri);
        }
    }
}
