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
            if (!employeesProvider.Get(command.Employee.Id).IsNull()) throw new InvalidDataException("Empleado ya Existe");
            employeesProvider.Save(command.Employee);

        }
    }
}
