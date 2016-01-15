using Patterns.Api.Contracts;
using Patterns.Api.Models;

namespace Patterns.Api.Commands
{
    public class GetEmployeeCommand : Command<Employee>
    {
        public int Id { get; private set; }

        public GetEmployeeCommand(int id)
        {
            this.Id = id;
        }
    }
}
