using etl.DTOs;
using etl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etl.Mappers
{
    internal class BreakMapper
    {
        public static Break MapDTOToModel(BreakDTO breakDTO, ShiftDTO shiftDTO)
        {
            Break ret = new Break() {
                BreakId = breakDTO.BreakId,
                BreakStart = breakDTO.BreakStart,
                BreakFinish = breakDTO.BreakFinish,
                IsPaid = breakDTO.IsPaid,
                ShiftId = shiftDTO.ShiftId,
            };

            return ret;
        }
    }
}
