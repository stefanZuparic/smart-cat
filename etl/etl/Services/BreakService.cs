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
    internal class BreakService
    {
        BreakRepository breakRepository = new BreakRepository();

        public void Save(BreakDto breakDto, ShiftDto shiftDto)
        {
            Break br = BreakMapper.MapDtoToModel(breakDto, shiftDto);

            if (breakRepository.Get(breakDto.BreakId) == null)
                breakRepository.Inser(br);
        }

        public List<BreakDto> ConvertJsonToBreakDto(JToken shift)
        {
            List<BreakDto> breaks = new List<BreakDto>(); 

            foreach (var br in shift["breaks"])
            {
                BreakDto breakDto = new BreakDto()
                {
                    BreakId = (Guid)br["id"],
                    BreakStart = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(br["start"].ToString())).DateTime,
                    BreakFinish = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(br["finish"].ToString())).DateTime,
                    IsPaid = (bool)br["paid"]
                };

                breaks.Add(breakDto);
            }

            return breaks;
        }

        public int TotalNumberOfPaidBreaks()
        {
            List<Break> paidBreaks = breakRepository.GetAll().Where(br => br.IsPaid == true).ToList();

            return paidBreaks.Count;
        }
    }
}
