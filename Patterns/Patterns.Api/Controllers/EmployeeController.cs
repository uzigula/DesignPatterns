using System;
using System.Collections.Generic;
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
        private readonly CommandHandler<GetAllEmployeesCommand, List<Employee>> getAllCommand;
        private readonly CommandHandler<GetEmployeeCommand, Employee> getCommand;
        private readonly CommandHandler<SaveEmployeeCommand> saveCommand;
        private readonly CommandHandler<UpdateEmployeeCommand> updateCommand;

        public EmployeeController(CommandHandler<GetAllEmployeesCommand,List<Employee>> getAllCommand,
                                  CommandHandler<GetEmployeeCommand, Employee> getCommand,
                                  CommandHandler<SaveEmployeeCommand> saveCommand,
                                  CommandHandler<UpdateEmployeeCommand> updateCommand)
        {
            this.getAllCommand = getAllCommand;
            this.getCommand = getCommand;
            this.saveCommand = saveCommand;
            this.updateCommand = updateCommand;
        }
        [Route("")]
        public IHttpActionResult Get()
        {
            var list = getAllCommand.Handle(new GetAllEmployeesCommand());
            return Ok(list);
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var employee = getCommand.Handle(new GetEmployeeCommand(id));
            return Ok(employee);
        }


        [Route("")]
        public IHttpActionResult Post(Employee newEmployee)
        {
            saveCommand.Handle(new SaveEmployeeCommand(newEmployee));
            return Ok();
        }


        [Route("{id:int}")]
        public IHttpActionResult Put(int id, Employee employee)
        {
            updateCommand.Handle(new UpdateEmployeeCommand(id, employee));
            return Ok();

        }


    }
}