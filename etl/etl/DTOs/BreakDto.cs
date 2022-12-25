using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etl.DTOs
{
    internal class BreakDTO
    {
        public Guid BreakId { get; set; }

        public DateTime BreakStart { get; set; }

        public DateTime BreakFinish { get; set; }

        public bool? IsPaid { get; set; }
    }
}
