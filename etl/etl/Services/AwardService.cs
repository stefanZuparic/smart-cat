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

        public void Save(AwardDTO awardDTO, ShiftDTO shiftDTO)
        {
            AwardInterpretation award = AwardMapper.MapDTOToModel(awardDTO, shiftDTO);

            AwardInterpretation? existing = awardRepository.Get(awardDTO.AwardId);

            if (existing == null)
                awardRepository.Insert(award);
        }

        public List<AwardDTO> ConvertJsonToAwardDTO(JToken shift)
        {
            List<AwardDTO> awards = new List<AwardDTO>();

            foreach (var award in shift["award_interpretations"])
            {
                AwardDTO awardDTO = new AwardDTO()
                {
                    AwardId = (Guid)award["id"],
                    AwardDate = DateOnly.Parse(award["date"].ToString()),
                    AwardUnits = double.Parse(award["units"].ToString()),
                    AwardCost = decimal.Parse(award["cost"].ToString())
                };

                awards.Add(awardDTO);
            };

            return awards;
        }
    }
}
