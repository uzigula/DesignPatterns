using System.Linq;
using FluentValidation;
using Patterns.Api.Contracts;
using Patterns.Api.Exceptions;

namespace Patterns.Api.CrossCuttingConcerns
{
    public class ValidatorHandlerDecorator<TCommand, TResponse> : CommandHandler<TCommand, TResponse>
     where TCommand : Command<TResponse>
    {
        private readonly CommandHandler<TCommand, TResponse> innerHandler;
        private readonly IValidator<TCommand>[] validators;

        public ValidatorHandlerDecorator(CommandHandler<TCommand, TResponse> innerHandler, IValidator<TCommand>[] validators)
        {
            this.innerHandler = innerHandler;
            this.validators = validators;
        }

        public TResponse Handle(TCommand request)
        {
            var context = new ValidationContext(request);
            var failures = validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
                throw new CustomValidationException(failures);

            return innerHandler.Handle(request);
        }
    }
}