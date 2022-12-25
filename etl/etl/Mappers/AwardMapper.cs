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
        public static AwardInterpretation MapDtoToModel(AwardDto awardDto, ShiftDto shiftDto)
        {
            AwardInterpretation award = new AwardInterpretation() { 
                AwardId = awardDto.AwardId,
                AwardCost = awardDto.AwardCost,
                AwardDate = awardDto.AwardDate,
                AwardUnits = awardDto.AwardUnits,
                ShiftId = shiftDto.ShiftId
            };

            return award;
        }
    }
}
