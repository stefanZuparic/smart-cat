using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etl.DTOs
{
    internal class ShiftDTO
    {
        public Guid ShiftId { get; set; }

        public DateOnly ShiftDate { get; set; }

        public DateTime ShiftStart { get; set; }

        public DateTime ShiftFinish { get; set; }

        public decimal? ShiftCost { get; set; }

        public List<BreakDTO> breakDTOs { get; set; }

        public List<AllowanceDTO> allowanceDTOs { get; set; }

        public List<AwardDTO> awardDTOs { get; set; }

    }
}
