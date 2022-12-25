using etl.DTOs;
using etl.Mappers;
using etl.Models;
using etl.Repositores;
using Newtonsoft.Json.Linq;
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

            if(awardRepository.Get(awardDto.AwardId) == null)
                awardRepository.Inser(award);
        }

        public List<AwardDto> ConvertJsonToAwardDto(JToken shift)
        {
            List<AwardDto> awards = new List<AwardDto>();

            foreach (var award in shift["award_interpretations"])
            {
                AwardDto awardDto = new AwardDto()
                {
                    AwardId = (Guid)award["id"],
                    AwardDate = DateOnly.Parse(award["date"].ToString()),
                    AwardUnits = double.Parse(award["units"].ToString()),
                    AwardCost = decimal.Parse(award["cost"].ToString())
                };

                awards.Add(awardDto);
            };

            return awards;
        }
    }
}
