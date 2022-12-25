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

        public void Save(AllowanceDTO allowanceDTO, ShiftDTO shiftDTO) 
        {
            Allowance allowance = AllowanceMapper.MapDTOToModel(allowanceDTO, shiftDTO);

            Allowance? existing = allowanceRepository.Get(allowanceDTO.AllowanceId);

            if (existing == null)
                allowanceRepository.Insert(allowance);
        }

        public List<AllowanceDTO> ConvertJsonToAllowanceDTO(JToken shift)
        { 
            List<AllowanceDTO> allowances = new List<AllowanceDTO>();

            foreach (var allowance in shift["allowances"])
            {
                if (Guid.TryParse(allowance["id"].ToString(),out Guid id) && 
                    double.TryParse(allowance["value"].ToString(), out double value) && 
                    decimal.TryParse(allowance["cost"].ToString(), out decimal cost))
                {
                    AllowanceDTO allowanceDTO = new AllowanceDTO()
                    {
                        AllowanceId = id,
                        AllowanceValue = value,
                        AllowanceCost = cost
                    };

                    allowances.Add(allowanceDTO);
                }
            }

            return allowances;
        }

        public decimal MaxAllowanceCost14Days()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            List<Allowance> allowancesInLast14Days = allowanceRepository.GetAll()
                                                                        .Where(a => a.Shift.ShiftDate <= today && a.Shift.ShiftDate >= today.AddDays(-14))
                                                                        .OrderBy(a => a.Shift.ShiftDate)
                                                                        .ToList();
            if(allowancesInLast14Days.Count > 0)
                return (decimal)allowancesInLast14Days.Max(a => a.AllowanceValue);

            return 0;
        }

    }
}
