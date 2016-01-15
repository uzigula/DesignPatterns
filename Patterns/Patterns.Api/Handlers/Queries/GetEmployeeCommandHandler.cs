using System;
using System.Management.Instrumentation;
using Patterns.Api.Contracts;
using Patterns.Api.Models;
using Patterns.Api.Requests.Queries;

namespace Patterns.Api.Handlers.Queries
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
