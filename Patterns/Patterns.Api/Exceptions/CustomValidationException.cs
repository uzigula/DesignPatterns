using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Patterns.Api.Exceptions
{
    public class CustomValidationException : Exception
    {
        public IList<ValidationFailure> Failures { get; set; }

        public CustomValidationException(IList<ValidationFailure> failures)
        {
            this.Failures = failures;
        }

        public override string Message
        {
            get { return Failures.Any() ? this.Failures.First().ErrorMessage : string.Empty; }
        }

    }
}