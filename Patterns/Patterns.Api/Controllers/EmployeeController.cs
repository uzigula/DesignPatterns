using System;
using System.Web.Http;
using Patterns.Api.Contracts;
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
            var list = employeesProvider.GetAll();
            if (list.IsNullOrEmpty()) return NotFound(); 
            return Ok(list);

        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var employee = employeesProvider.Get(id);
            if (employee.IsNull()) return NotFound();
            return Ok(employee);

        }


        [Route("")]
        public IHttpActionResult Post(Employee newEmployee)
        {
            if (newEmployee.Name.IsNullOrEmpty()) return BadRequest("Falto ingresar el Nombre");
            if (newEmployee.Position.IsNullOrEmpty()) return BadRequest("Falto ingresar el puesto");
            if (!employeesProvider.Get(newEmployee.Id).IsNull()) return BadRequest("Empleado ya Existe");

            employeesProvider.Save(newEmployee);
            return Ok();
        }


        [Route("{id:int}")]
        public IHttpActionResult Put(int id, Employee employee)
        {
            if (employee.Name.IsNullOrEmpty()) return BadRequest("Falto ingresar el Nombre");
            if (employee.Position.IsNullOrEmpty()) return BadRequest("Falto ingresar el puesto");

            var employeeOld = employeesProvider.Get(id);
            if (employeeOld.IsNull()) return NotFound();

            employee.Id = id;
            employeesProvider.Update(employee);
            return Ok();
        }


    }
}