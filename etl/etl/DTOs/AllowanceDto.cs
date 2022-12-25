using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etl.DTOs
{
    internal class AllowanceDTO
    {
        public Guid AllowanceId { get; set; }

        public double AllowanceValue { get; set; }

        public decimal AllowanceCost { get; set; }
    }
}
