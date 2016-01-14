using System;
using System.IO;
using System.Management.Instrumentation;
using System.Web.Http;
using Patterns.Api.Commands;
using Patterns.Api.Contracts;
using Patterns.Api.Handlers;
using Patterns.Api.Models;

namespace Patterns.Api.Controllers
{
    [RoutePrefix("Employee")]
    public class EmployeeController : ApiController
    {
        private readonly DataProvider<Employee> employeesProvider;

        public EmployeeController(DataProvider<Employee> provider)
        {
            this.employeesProvider = provider;
        }
        [Route("")]
        public IHttpActionResult Get()
        {
            var list = new GetAllEmployeesCommandHandler(employeesProvider).Handle(new GetAllEmployeesCommand());
            return Ok(list);
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var employee = new GetEmployeeCommandHandler(employeesProvider).Handle(new GetEmployeeCommand(id));
            return Ok(employee);
        }


        [Route("")]
        public IHttpActionResult Post(Employee newEmployee)
        {
            new SaveEmployeeCommandHandler(employeesProvider).Handle(new SaveEmployeeCommand(newEmployee));
            return Ok();
        }


        [Route("{id:int}")]
        public IHttpActionResult Put(int id, Employee employee)
        {
            new UpdateEmployeeCommandHandler(employeesProvider).Handle(new UpdateEmployeeCommand(id, employee));
            return Ok();

        }


    }
}