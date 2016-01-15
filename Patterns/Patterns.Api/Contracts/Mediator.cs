using System;

namespace Patterns.Api.Contracts
{
    public interface IMediator
    {
        TResponse Send<TResponse>(Command<TResponse> request);
    }
    
    public delegate object Resolver(Type serviceType);

    public class Mediator : IMediator
    {
        private readonly Resolver resolver;

        public Mediator(Resolver singleInstanceFactory)
        {
            resolver = singleInstanceFactory;
        }

        public TResponse Send<TResponse>(Command<TResponse> request)
        {
            var defaultHandler = GetHandler(request);

            TResponse result = defaultHandler.Handle(request);

            return result;
        }

        private static InvalidOperationException BuildException(object message, Exception inner = null)
        {
            return
                new InvalidOperationException(
                    "Handler was not found for request of type " + message.GetType() +
                    ".\r\nContainer or service locator not configured properly or handlers not registered with your container.",
                    inner);
        }

        private CommandHandler<TResponse> GetHandler<TResponse>(Command<TResponse> request)
        {
            var handlerType = typeof(CommandHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var wrapperType = typeof(WrapCommandHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            object handler;
            try
            {
                handler = resolver(handlerType);

                if (handler == null)
                    throw BuildException(request);
            }
            catch (Exception e)
            {
                throw BuildException(request, e);
            }
            var wrapperHandler = Activator.CreateInstance(wrapperType, handler);
            return (CommandHandler<TResponse>)wrapperHandler;
        }


        private abstract class CommandHandler<TResult>
        {
            public abstract TResult Handle(Command<TResult> message);
        }

        private class WrapCommandHandler<TCommand, TResult> : CommandHandler<TResult> where TCommand : Command<TResult>
        {
            private readonly CommandHandler<TCommand, TResult> _inner;

            public WrapCommandHandler(CommandHandler<TCommand, TResult> inner)
            {
                _inner = inner;
            }

            public override TResult Handle(Command<TResult> message)
            {
                return _inner.Handle((TCommand)message);
            }
        }
    }
}
