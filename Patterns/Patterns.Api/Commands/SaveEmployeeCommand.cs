using Patterns.Api.Contracts;
using Patterns.Api.Models;

namespace Patterns.Api.Commands
{
    public class SaveEmployeeCommand : Command
    {
        public Employee Employee { get; private set; }

        public SaveEmployeeCommand(Employee newEmployee)
        {
            this.Employee = newEmployee;
        }
    }
}
