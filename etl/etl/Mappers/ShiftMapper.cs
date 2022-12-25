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
        public static Shift MapDTOToModel(ShiftDTO shiftDTO) 
        {
            Shift shift = new Shift() { 
                ShiftId = shiftDTO.ShiftId,
                ShiftDate = shiftDTO.ShiftDate,
                ShiftFinish = shiftDTO.ShiftFinish,
                ShiftStart = shiftDTO.ShiftStart,
                ShiftCost = shiftDTO.ShiftCost,
            };

            return shift;
        }
    }
}
