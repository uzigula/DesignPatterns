using System;
using System.Linq;
using System.Web.Http;
using FluentValidation;
using Newtonsoft.Json.Serialization;
using Patterns.Api.Contracts;
using Patterns.Api.CrossCuttingConcerns;
using Patterns.Api.RulesImpl;
using Patterns.Api.Validators;
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
            container.Register(typeof(DataProvider<>), 
                new[] { typeof(DataProvider<>).Assembly }, Lifestyle.Singleton);
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


            container.RegisterDecorator(
                typeof (CommandHandler<,>),
                typeof (ValidatorHandlerDecorator<,>));

            container.RegisterDecorator(
                typeof(CommandHandler<,>),
                typeof(TransactionHandlerDecorator<,>), c => c.ImplementationType.Namespace.Contains("Commands")
                );

            container.RegisterDecorator(
                typeof (CommandHandler<,>),
                typeof (AuditTrailHandlerDecorator<,>),
                d => d.ImplementationType.GetInterfaces().Contains(typeof(Auditable))
                );

        }

        public static void ConfigureValidators(Container container)
        {
            
            container.Register(typeof(IValidator<>),
                new[] { typeof(SaveEmployeeValidator).Assembly });

            var repositoryAssembly = typeof(SaveEmployeeValidator).Assembly;
            
            var requests =
                from type in repositoryAssembly.GetExportedTypes()
                where type.Namespace.Contains("Patterns.Api.Requests")
                select type;

            foreach (var request in requests)
            {
                var taskTypes = from type in repositoryAssembly.GetTypes()
                                where typeof(IValidator<>).MakeGenericType(request).IsAssignableFrom(type)
                                      && !type.IsAbstract && !type.IsGenericTypeDefinition
                                select type;

                if (!taskTypes.Any()) continue;

                taskTypes.ForEach(type => container.Register(type, type, Lifestyle.Transient));

                // registers a list of all those (singleton) tasks.
                var handler = typeof(IValidator<>).MakeGenericType(request);
                container.RegisterCollection(handler, taskTypes);
            }

        }
    }
}