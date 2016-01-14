using System;
using System.IO;
using Patterns.Api.Commands;
using Patterns.Api.Contracts;
using Patterns.Api.Models;

namespace Patterns.Api.Handlers
{
    public class SaveEmployeeCommandHandler : CommandHandler<SaveEmployeeCommand>
    {
        private DataProvider<Employee> employeesProvider;

        public SaveEmployeeCommandHandler(DataProvider<Employee> employeesProvider)
        {
            this.employeesProvider = employeesProvider;
        }

        protected override void HandleCore(SaveEmployeeCommand command)
        {

            if (command.Employee.Name.IsNullOrEmpty()) throw new InvalidDataException( "Falto ingresar el Nombre");
            if (command.Employee.Position.IsNullOrEmpty()) throw new InvalidDataException("Falto ingresar el puesto");
            if (!employeesProvider.Get(command.Employee.Id).IsNull()) throw new InvalidDataException("Empleado ya Existe");
            employeesProvider.Save(command.Employee);

        }
    }
}
