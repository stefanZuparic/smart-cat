using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using etl.DTOs;
using etl.Mappers;
using etl.Models;
using etl.Repositores;

namespace etl.Services
{
    internal class AllowanceService
    {
        AllowanceRepository allowanceRepository = new AllowanceRepository();

        public void Save(AllowanceDto allowanceDto, ShiftDto shiftDto) {
            Allowance allowance = AllowanceMapper.MapDtoToModel(allowanceDto, shiftDto);
            allowanceRepository.Inser(allowance);
        }
    }
}
