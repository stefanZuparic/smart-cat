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
        public static Allowance MapDTOToModel(AllowanceDTO allowanceDTO, ShiftDTO shiftDTO)
        { 
            Allowance allowance = new Allowance() { 
                AllowanceId = allowanceDTO.AllowanceId,
                AllowanceCost = allowanceDTO.AllowanceCost,
                AllowanceValue = allowanceDTO.AllowanceValue,
                ShiftId = shiftDTO.ShiftId,
            };

            return allowance;
        }
    }
}
