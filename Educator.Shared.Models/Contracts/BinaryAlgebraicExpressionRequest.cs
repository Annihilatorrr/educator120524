using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educator.Shared.Models.Contracts
{
    public class BinaryAlgebraicExpressionRequest
    {
        //[FromQuery(Name = "allowedOperators")]
        public required List<string> AllowedOperators { get; set; }

        //[FromQuery(Name = "minOperand")]
        public int MinOperand { get; set; }

        //[FromQuery(Name = "maxOperand")]
        public int MaxOperand { get; set; }

       // [FromQuery(Name = "minAnswer")]
        public int MinAnswer { get; set; }

       // [FromQuery(Name = "maxAnswer")]
        public int MaxAnswer { get; set; }

       // [FromQuery(Name = "count")]
        public int Count { get; set; }
    }
}
