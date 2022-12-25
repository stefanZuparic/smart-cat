using etl.DTOs;
using etl.Mappers;
using etl.Models;
using etl.Repositores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etl.Services
{
    internal class AwardService
    {
        AwardRepository awardRepository = new AwardRepository();

        public void Save(AwardDto awardDto, ShiftDto shiftDto)
        {
            AwardInterpretation award = AwardMapper.MapDtoToModel(awardDto, shiftDto);
            awardRepository.Inser(award);
        }
    }
}
