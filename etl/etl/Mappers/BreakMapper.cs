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
        public static Break MapDtoToModel(BreakDto breakDto, ShiftDto shiftDto)
        {
            Break ret = new Break() {
                BreakId = breakDto.BreakId,
                BreakStart = breakDto.BreakStart,
                BreakFinish = breakDto.BreakFinish,
                IsPaid = breakDto.IsPaid,
                ShiftId = shiftDto.ShiftId,
            };

            return ret;
        }
    }
}
