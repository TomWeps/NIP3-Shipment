using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Owin;
using NIP3.Shipment.App_Start;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.WebApi;

[assembly: OwinStartup(typeof(NIP3.Shipment.Startup))]

namespace NIP3.Shipment
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Configuration(app, UnityConfig.Init());
        }

        internal void Configuration(IAppBuilder app, IUnityContainer container)
        {
            var config = new HttpConfiguration();
            config.DependencyResolver = new UnityHierarchicalDependencyResolver(container);

            WebApiConfig.Register(config);
            SwaggerConfig.Register(config);

            app.UseWebApi(config);
        }
    }
}
