using System;
using System.IO;
using Patterns.Api.Contracts;

namespace Patterns.Api.CrossCuttingConcerns
{
    public class AuditTrailHandlerDecorator<TCommand, TResponse> : CommandHandler<TCommand, TResponse>
        where TCommand : Command<TResponse>
    {
        private readonly CommandHandler<TCommand, TResponse> innerCommandHdl;
        private readonly TextWriter writer;

        public AuditTrailHandlerDecorator(CommandHandler<TCommand, TResponse> inner, TextWriter writer)
        {
            this.innerCommandHdl = inner;
            this.writer = writer;
        }
        public TResponse Handle(TCommand command)
        {
            // Before AuditTrail Record Code
            writer.WriteLine("Audit Decorator Called");
            TResponse result = this.innerCommandHdl.Handle(command);
            //After AuditTrail Record Code
            return result;
        }

        
    }
}