using etl.DTOs;
using etl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etl.Mappers
{
    internal class AllowanceMapper
    {
        public static Allowance MapDtoToModel(AllowanceDto allowanceDto, ShiftDto shiftDto)
        { 
            Allowance allowance = new Allowance() { 
                AllowanceId = allowanceDto.AllowanceId,
                AllowanceCost = allowanceDto.AllowanceCost,
                AllowanceValue = allowanceDto.AllowanceValue,
                ShiftId = shiftDto.ShiftId,
            };

            return allowance;
        }
    }
}
