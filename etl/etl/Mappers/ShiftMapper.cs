using etl.DTOs;
using etl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etl.Mappers
{
    internal class ShiftMapper
    {
        public static Shift MapDtoToModel(ShiftDto shiftDto) 
        {
            Shift shift = new Shift() { 
                ShiftId = shiftDto.ShiftId,
                ShiftDate = shiftDto.ShiftDate,
                ShiftFinish = shiftDto.ShiftFinish,
                ShiftStart = shiftDto.ShiftStart,
                ShiftCost = shiftDto.ShiftCost,
            };

            return shift;
        }
    }
}
