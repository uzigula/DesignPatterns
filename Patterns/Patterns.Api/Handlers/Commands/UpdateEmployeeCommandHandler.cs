using System;
using System.IO;
using System.Management.Instrumentation;
using Patterns.Api.Contracts;
using Patterns.Api.Models;
using Patterns.Api.Requests.Commands;

namespace Patterns.Api.Handlers.Commands
{
    public class UpdateEmployeeCommandHandler : CommandHandler<UpdateEmployeeCommand>
    {
        private DataProvider<Employee> employeesProvider;

        public UpdateEmployeeCommandHandler(DataProvider<Employee> employeesProvider)
        {
            // TODO: Complete member initialization
            this.employeesProvider = employeesProvider;
        }
        protected override void HandleCore(UpdateEmployeeCommand command)
        {
            if (command.Employee.Name.IsNullOrEmpty()) throw  new InvalidDataException("Falto ingresar el Nombre");
            if (command.Employee.Position.IsNullOrEmpty()) throw new InvalidDataException("Falto ingresar el puesto");

            var employeeOld = employeesProvider.Get(command.Id);
            if (employeeOld.IsNull()) throw new InstanceNotFoundException();

            employeesProvider.Update(command.Employee);
        }
    }
}
