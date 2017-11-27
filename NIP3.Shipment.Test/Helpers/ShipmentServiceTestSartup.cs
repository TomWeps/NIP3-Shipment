using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using Microsoft.Practices.Unity;
using NIP3.Shipment.App_Start;
using NIP3.Shipment.Test.Helpers.Stubs;
using NIP3.Shipment.Domain;

namespace NIP3.Shipment.Test.Helpers
{
    public class ShipmentServiceTestSartup
    {
        public void Configuration(IAppBuilder app)
        {
            // Create stubs 
            var shipmentRequestValidator = new ShipmentRequestValidatorStub();
            var shipmentDispatcher = new ShipmentDispatcher();

            // init IoC
            IUnityContainer container = new UnityContainer();
            container.RegisterInstance<IShipmentRequestValidator>(shipmentRequestValidator);
            container.RegisterInstance<IShipmentDispatcher>(shipmentDispatcher);

            // init owin middleware - pact's state providers 
            var args = new Object[] {shipmentRequestValidator, shipmentDispatcher};
            app.Use<ShipmentServiceProviderStateMiddleware>(args);

            // owin middleware - this is standard service setup (real provider)
            var shipmentOwinStartup = new Shipment.Startup();
            shipmentOwinStartup.Configuration(app, container);
        }        
    }
}
