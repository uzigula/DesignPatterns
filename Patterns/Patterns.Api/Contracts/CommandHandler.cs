using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patterns.Api.Contracts
{
    public interface CommandHandler<TCommand, TReponse> where TCommand : Command<TReponse>
    {
        TReponse Handle(TCommand command);
    }

    public abstract class CommandHandler<TCommand> : CommandHandler<TCommand, Unit>
        where TCommand : Command
    {
        public Unit Handle(TCommand command)
        {
            HandleCore(command);

            return Unit.Value;
        }

        protected abstract void HandleCore(TCommand command);
    }
}
