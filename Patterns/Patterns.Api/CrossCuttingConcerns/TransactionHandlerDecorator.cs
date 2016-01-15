using System.Transactions;
using Patterns.Api.Contracts;

namespace Patterns.Api.CrossCuttingConcerns
{
    public class TransactionHandlerDecorator<TCommand, TResponse> : CommandHandler<TCommand, TResponse>
        where TCommand : Command<TResponse>
    {
        private readonly CommandHandler<TCommand, TResponse> innerHandler;

        public TransactionHandlerDecorator(CommandHandler<TCommand, TResponse> innerHandler)
        {
            this.innerHandler = innerHandler;

        }

        public TResponse Handle(TCommand message)
        {
            using (var transaction = new TransactionScope())
            {
                var result = this.innerHandler.Handle(message);
                transaction.Complete();
                return result;
            }
        }

    }
}