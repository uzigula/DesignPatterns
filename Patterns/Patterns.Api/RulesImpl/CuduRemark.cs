using System.Text;
using Patterns.Api.Contracts;
using Patterns.Api.Models;

namespace Patterns.Api.RulesImpl
{
    public class CuduRemark :Remark
    {
        public string Apply(Claim claim)
        {
            var result = new StringBuilder();
            if (claim.Contigency) result.AppendLine("Contigency Amount");
            if (claim.Unliquidated) result.AppendLine("Unliquidated Amount");
            if (claim.Undetermined && claim.TotalAmount !=0) result.AppendLine("Undeterminated Amount");

            return result.ToString();
        }
    }
}