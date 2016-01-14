using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Patterns.Api.Contracts;
using Patterns.Api.RulesImpl;
using SimpleInjector;

namespace Patterns.Api.CompositionRoot
{
    public class SimpleInjectorConfiguration
    {
        public static void ConfigureWebApp(HttpConfiguration config, Container container)
        {
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            container.RegisterWebApiControllers(config);
            container.EnableHttpRequestMessageTracking(config);
        }

        public static void ConfigureRemarks(Container container)
        {
            
            var assembly = typeof(ClaimedRemark).Assembly;

            var types = from type in assembly.GetTypes()
                        where (typeof(Remark).IsAssignableFrom(type))
                        where type.IsClass
                        select type;

            if (!types.Any()) return;

            container.RegisterCollection(typeof(Remark), types);


        }

        public static void ConfigureGenerics(Container container)
        {
            container.Register(typeof(DataProvider<>), new[] { typeof(DataProvider<>).Assembly });

            
        }
    }
}