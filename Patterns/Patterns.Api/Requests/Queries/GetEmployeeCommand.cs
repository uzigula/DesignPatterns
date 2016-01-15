using Patterns.Api.Contracts;
using Patterns.Api.Models;

namespace Patterns.Api.Requests.Queries
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
