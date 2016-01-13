using Owin;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;

namespace Patterns.Api.CompositionRoot
{
    public static class SimpleInjectorOwinExtensions
    {
        public static void UseOwinContextSimpleInjector(this IAppBuilder app, Container container)
        {
            app.Use(async (context, next) =>
            {
                using (var scope = container.BeginExecutionContextScope())
                {
                    await next.Invoke();
                }
            });
        }
    }
}