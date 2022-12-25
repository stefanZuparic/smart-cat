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

        public void Save(BreakDTO breakDTO, ShiftDTO shiftDTO)
        {
            Break br = BreakMapper.MapDTOToModel(breakDTO, shiftDTO);

            if (breakRepository.Get(breakDTO.BreakId) == null)
                breakRepository.Insert(br);
        }

        public List<BreakDTO> ConvertJsonToBreakDTO(JToken shift)
        {
            List<BreakDTO> breaks = new List<BreakDTO>(); 

            foreach (var br in shift["breaks"])
            {
                if (Guid.TryParse(br["id"].ToString(), out Guid id) &&
                    long.TryParse(br["start"].ToString(), out long start) &&
                    long.TryParse(br["finish"].ToString(), out  long finish) &&
                    bool.TryParse(br["paid"].ToString(), out bool paid)
                    ) 
                {
                    BreakDTO breakDTO = new BreakDTO()
                    {
                        BreakId = id,
                        BreakStart = DateTimeOffset.FromUnixTimeMilliseconds(start).DateTime,
                        BreakFinish = DateTimeOffset.FromUnixTimeMilliseconds(finish).DateTime,
                        IsPaid = paid
                    };

                    breaks.Add(breakDTO);
                }
            }

            return breaks;
        }

        public int TotalNumberOfPaidBreaks()
        {
            List<Break> paidBreaks = breakRepository.GetAll()
                                                    .Where(br => br.IsPaid == true)
                                                    .ToList();

            return paidBreaks.Count;
        }

        public decimal MeanBreakLengthInMinutes()
        {
            List<Break> breaks = breakRepository.GetAll();
            decimal minutes = 0;

            if (breaks.Count > 0)
            {
                foreach (Break br in breaks)
                {
                    minutes += (decimal)(br.BreakFinish - br.BreakStart).TotalMinutes;
                }

                return (decimal)(minutes / breaks.Count());
            }

            return 0;
        }
    }
}
