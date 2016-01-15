using System.IO;
using FluentValidation;
using Patterns.Api.Commands;

namespace Patterns.Api.Validators
{
    public class SaveEmployeeValidator : AbstractValidator<SaveEmployeeCommand>
    {
        public SaveEmployeeValidator()
        {
            RuleFor(p => p.Employee.Name).NotNull().NotEmpty().WithMessage("Falto ingresar el nombre");
            RuleFor(p => p.Employee.Position).NotNull().NotEmpty().WithMessage("Falto ingresar el puesto");
        }
    }
}