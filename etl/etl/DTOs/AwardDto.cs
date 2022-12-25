using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etl.DTOs
{
    internal class AwardDTO
    {
        public Guid AwardId { get; set; }

        public DateOnly AwardDate { get; set; }

        public double AwardUnits { get; set; }

        public decimal AwardCost { get; set; }
    }
}
