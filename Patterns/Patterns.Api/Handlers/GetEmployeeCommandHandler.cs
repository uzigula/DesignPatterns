using System;
using System.Management.Instrumentation;
using Patterns.Api.Commands;
using Patterns.Api.Contracts;
using Patterns.Api.Models;

namespace Patterns.Api.Handlers
{
    public class GetEmployeeCommandHandler : CommandHandler<GetEmployeeCommand,Employee>
    {
        private readonly DataProvider<Employee> employeesProvider;

        public GetEmployeeCommandHandler(DataProvider<Employee> employeesProvider)
        {
            this.employeesProvider = employeesProvider;
        }
        public Employee Handle(GetEmployeeCommand command)
        {
            var result = employeesProvider.Get(command.Id);
            if (result.IsNull()) throw new InstanceNotFoundException();

            return result;
        }
    }
}
