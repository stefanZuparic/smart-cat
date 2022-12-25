using etl.DTOs;
using etl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etl.Mappers
{
    internal class AwardMapper
    {
        public static AwardInterpretation MapDTOToModel(AwardDTO awardDTO, ShiftDTO shiftDTO)
        {
            AwardInterpretation award = new AwardInterpretation() { 
                AwardId = awardDTO.AwardId,
                AwardCost = awardDTO.AwardCost,
                AwardDate = awardDTO.AwardDate,
                AwardUnits = awardDTO.AwardUnits,
                ShiftId = shiftDTO.ShiftId
            };

            return award;
        }
    }
}
