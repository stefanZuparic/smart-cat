using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etl.DTOs
{
    internal class ShiftDto
    {
        public Guid ShiftId { get; set; }

        public DateOnly ShiftDate { get; set; }

        public DateTime ShiftStart { get; set; }

        public DateTime ShiftFinish { get; set; }

        public decimal? ShiftCost { get; set; }

        public List<BreakDto> breakDtos { get; set; }

        public List<AllowanceDto> allowanceDtos { get; set; }

        public List<AwardDto> awardDtos { get; set; }

    }
}
