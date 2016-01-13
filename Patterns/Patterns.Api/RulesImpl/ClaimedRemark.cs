using Patterns.Api.Contracts;
using Patterns.Api.Models;

namespace Patterns.Api.RulesImpl
{
    public class ClaimedRemark : Remark
    {
        public string Apply(Claim claim)
        {
            if (claim.Claimed) return "Claimed Amount";
            return string.Empty;
        }
    }
}