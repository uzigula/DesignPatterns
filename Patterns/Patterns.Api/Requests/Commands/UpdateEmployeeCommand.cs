using Patterns.Api.Contracts;
using Patterns.Api.Models;

namespace Patterns.Api.Requests.Commands
{
    public class UpdateEmployeeCommand : Command
    {
        public int Id { get; private set; }
        public Employee Employee { get; private set; }

        public UpdateEmployeeCommand(int id, Models.Employee employee)
        {
            this.Id = id;
            this.Employee = employee;
            this.Employee.Id = id;
        }
    }
}
