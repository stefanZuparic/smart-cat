using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using etl.DTOs;
using etl.Mappers;
using etl.Models;
using etl.Repositores;
using Newtonsoft.Json.Linq;

namespace etl.Services
{
    internal class AllowanceService
    {
        AllowanceRepository allowanceRepository = new AllowanceRepository();

        public void Save(AllowanceDto allowanceDto, ShiftDto shiftDto) {
            Allowance allowance = AllowanceMapper.MapDtoToModel(allowanceDto, shiftDto);
            allowanceRepository.Inser(allowance);
        }

        public List<AllowanceDto> ConvertJsonToAllowanceDto(JToken shift)
        { 
            List<AllowanceDto> allowances = new List<AllowanceDto>();

            foreach (var allowance in shift["allowances"])
            {
                AllowanceDto allowanceDto = new AllowanceDto()
                {
                    AllowanceId = (Guid)allowance["id"],
                    AllowanceValue = double.Parse(allowance["value"].ToString()),
                    AllowanceCost = decimal.Parse(allowance["cost"].ToString())
                };

                allowances.Add(allowanceDto);
            }

            return allowances;
        }

    }
}
