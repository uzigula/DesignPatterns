using Patterns.Api.Models;
namespace Patterns.Api.Contracts
{
    public interface Remark
    {
        string Apply(Claim claim);
    }
}