using System.Web.Http;
using Patterns.Api.Commands;
using Patterns.Api.Contracts;
using Patterns.Api.CrossCuttingConcerns;
using Patterns.Api.Handlers;
using Patterns.Api.Impl;
using Patterns.Api.Models;
using Patterns.Api.Validators;

namespace Patterns.Api.Controllers
{
    [RoutePrefix("Employee")]
    public class EmployeeController : ApiController
    {
        private readonly IMediator mediator;

        public EmployeeController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [Route("")]
        public IHttpActionResult Get()
        {
            var list = mediator.Send(new GetAllEmployeesCommand());
            return Ok(list);
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var employee = mediator.Send(new GetEmployeeCommand(id)); 
            return Ok(employee);
        }


        [Route("")]
        public IHttpActionResult Post(Employee newEmployee)
        {
            mediator.Send(new SaveEmployeeCommand(newEmployee));
            return Ok();
        }


        [Route("{id:int}")]
        public IHttpActionResult Put(int id, Employee employee)
        {
            mediator.Send(new UpdateEmployeeCommand(id, employee));
            return Ok();

        }


    }
}