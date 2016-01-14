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
            try
            {
                var list = new GetAllEmployeesCommandHandler(employeesProvider).Handle(new GetAllEmployeesCommand());
                return Ok(list);
            }
            catch (InstanceNotFoundException e)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var employee = new GetEmployeeCommandHandler(employeesProvider).Handle(new GetEmployeeCommand(id));
                return Ok(employee);

            }
            catch (InstanceNotFoundException e)
            {
                return NotFound();
            }

            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        [Route("")]
        public IHttpActionResult Post(Employee newEmployee)
        {
            try
            {
                new SaveEmployeeCommandHandler(employeesProvider).Handle(new SaveEmployeeCommand(newEmployee));
                return Ok();

            }
            catch (InvalidDataException e)
            {

                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }


        [Route("{id:int}")]
        public IHttpActionResult Put(int id, Employee employee)
        {
            try
            {
                new UpdateEmployeeCommandHandler(employeesProvider).Handle(new UpdateEmployeeCommand(id, employee));

                return Ok();
            }
            catch (InstanceNotFoundException ex)
            {
                return NotFound();
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


    }
}