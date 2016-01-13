using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patterns.Api.Models
{
    public class Claim
    {
        public bool Claimed { get; set; }

        public bool Contigency { get; set; }

        public bool Unliquidated { get; set; }

        public bool Undetermined { get; set; }

        public int TotalAmount { get; set; }
    }
}
