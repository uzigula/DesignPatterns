using System.Collections.Generic;
using System.Linq;
using Patterns.Api.Contracts;
using Patterns.Api.Models;

namespace Patterns.Api.Impl
{
    public class EmployeeDataProvider : DataProvider<Employee>
    {
        private static List<Employee> employees = new List<Employee>();

        public List<Employee> GetAll()
        {
            return employees.ToList();
        }

        public Employee Get(int id)
        {
            return employees.FirstOrDefault(x => x.Id == id);
        }

        public void Save(Employee item)
        {
            employees.Add(item);
        }

        public void Update(Employee employee)
        {
            var current = employees.FirstOrDefault(x => x.Id == employee.Id);
            current.Name = employee.Name;
            current.Position = employee.Position;

        }
    }
}