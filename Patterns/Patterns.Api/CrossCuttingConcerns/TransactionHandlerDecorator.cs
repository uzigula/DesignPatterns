using System.IO;
using System.Transactions;
using Patterns.Api.Contracts;

namespace Patterns.Api.CrossCuttingConcerns
{
    public class TransactionHandlerDecorator<TCommand, TResponse> : CommandHandler<TCommand, TResponse>
        where TCommand : Command<TResponse>
    {
        private readonly CommandHandler<TCommand, TResponse> innerHandler;
        private readonly TextWriter writer;

        public TransactionHandlerDecorator(CommandHandler<TCommand, TResponse> innerHandler, TextWriter writer)
        {
            this.innerHandler = innerHandler;
            this.writer = writer;
        }

        public TResponse Handle(TCommand message)
        {
            using (var transaction = new TransactionScope())
            {
                writer.WriteLine("Empezando la transaccion");
                var result = this.innerHandler.Handle(message);
                transaction.Complete();
                writer.WriteLine("transaccion finalizada");
                return result;
            }
        }

    }
}