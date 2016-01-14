using System;
using System.Collections.Generic;
using System.Management.Instrumentation;
using Patterns.Api.Commands;
using Patterns.Api.Models;
using Patterns.Api.Contracts;

namespace Patterns.Api.Handlers
{
    public class GetAllEmployeesCommandHandler : CommandHandler<GetAllEmployeesCommand, List<Employee>>
    {
        private readonly DataProvider<Employee> employeesProvider;
        public GetAllEmployeesCommandHandler(DataProvider<Employee> provider )
        {
            this.employeesProvider = provider;
        }
        public List<Employee> Handle(GetAllEmployeesCommand command)
        {
            var result = employeesProvider.GetAll();
            if (result.IsNullOrEmpty()) throw new InstanceNotFoundException();
            return result;
        }
    }
}