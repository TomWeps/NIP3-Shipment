using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Practices.Unity;
using NIP3.Shipment.Domain;
using Microsoft.Practices.Unity.WebApi;

namespace NIP3.Shipment.App_Start
{
    public static class UnityConfig
    {
        public static IUnityContainer Init()
        {
            var container = new UnityContainer();

            RegisterTypes(container);

            return container;
        }

        internal static IUnityContainer RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IShipmentDispatcher, ShipmentDispatcher>(new ContainerControlledLifetimeManager());
            container.RegisterType<IShipmentRequestValidator, ShipmentRequestValidator>();

            return container;
        }
    }
}