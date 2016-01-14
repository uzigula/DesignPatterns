using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Microsoft.Owin.Cors;
using Owin;
using Patterns.Api.CompositionRoot;
using Patterns.Api.HttpHandlers;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace Patterns.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            var container = new Container();

            app.UseOwinContextSimpleInjector(container);

            SimpleInjectorConfiguration.ConfigureWebApp(config, container);
            SimpleInjectorConfiguration.ConfigureRemarks(container);
            SimpleInjectorConfiguration.ConfigureGenerics(container);

            config.Services.Replace(typeof(IExceptionHandler), new CustomExceptionHandler());
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);


            app.UseCors(CorsOptions.AllowAll);

            app.UseWebApi(config);

        }

    }
}