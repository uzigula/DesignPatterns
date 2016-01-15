using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Patterns.Api.Contracts;
using Patterns.Api.RulesImpl;
using SimpleInjector;
using SimpleInjector.Extensions;

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
            container.Register(typeof(DataProvider<>), new[] { typeof(DataProvider<>).Assembly }, Lifestyle.Singleton);
            container.Register(typeof(CommandHandler<,>), new[] { typeof(CommandHandler<,>).Assembly });
            container.Register(typeof(CommandHandler<>), new[] { typeof(CommandHandler<>).Assembly });

            var commandHandlerTypes = container.GetTypesToRegister(typeof(Command<>),
                new[] { typeof(Command<>).Assembly },
                    new TypesToRegisterOptions { IncludeGenericTypeDefinitions = true} );

            foreach (Type type in commandHandlerTypes.Where(t => t.IsGenericTypeDefinition))
            {
                container.RegisterConditional(typeof(Command<>), type, c => !c.Handled);
            }


        }

        public static void ConfigureMediator(Container container)
        {
            
            container.Register<IMediator>(() => new Mediator(container.GetInstance<Resolver>()));
            container.Register(() => Console.Out);
            container.Register<Resolver>(() => t => container.GetInstance(t));            
        }
    }
}