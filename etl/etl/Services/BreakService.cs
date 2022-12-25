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
    internal class BreakService
    {
        BreakRepository breakRepository = new BreakRepository();

        public void Save(BreakDto breakDto, ShiftDto shiftDto)
        {
            Break br = BreakMapper.MapDtoToModel(breakDto, shiftDto);
            breakRepository.Inser(br);
        }
    }
}
