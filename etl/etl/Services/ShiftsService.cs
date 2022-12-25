using etl.DTOs;
using etl.Helpers;
using etl.Mappers;
using etl.Models;
using etl.Repositores;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace etl.Services
{
    internal class ShiftsService
    {
        ShiftsRepository shiftsRepository = new ShiftsRepository();
        
        BreakService breakService = new BreakService();
        AllowanceService allowanceService = new AllowanceService();
        AwardService awardService = new AwardService();

        public async Task<List<ShiftDto>> LoadShift()
        { 
            List<ShiftDto> shifts = new List<ShiftDto>();

            string baseUrl = "http://localhost:8000";
            string FirstUrl = "/api/shifts?start=0&limit=30";

            while (FirstUrl != null) {
                using (HttpResponseMessage response = await ApiHelper.ApiShift.GetAsync(baseUrl + FirstUrl))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonRawData = JObject.Parse(await response.Content.ReadAsStringAsync());

                        ConvertJsonToShiftsDto(jsonRawData, ref shifts);

                        try
                        {
                            FirstUrl = jsonRawData["links"]["next"].ToString();
                        }
                        catch
                        {
                            FirstUrl = null;
                        }

                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
            }

            return shifts;
        }

        public List<ShiftDto> ConvertJsonToShiftsDto(JObject JsonRawData, ref List<ShiftDto> shifts)
        {

            foreach (var shift in JsonRawData["results"]) 
            {
                List<BreakDto> breaks = breakService.ConvertJsonToBreakDto(shift);   
                List<AwardDto> awards = awardService.ConvertJsonToAwardDto(shift);
                List<AllowanceDto> allowances = allowanceService.ConvertJsonToAllowanceDto(shift);

                ShiftDto shiftDto = new ShiftDto()
                {
                    ShiftId = (Guid)(shift["id"]),
                    ShiftDate = DateOnly.Parse(shift["date"].ToString()),
                    ShiftStart = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(shift["start"].ToString())).DateTime,
                    ShiftFinish = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(shift["finish"].ToString())).DateTime,
                    breakDtos = breaks,
                    allowanceDtos = allowances,
                    awardDtos = awards,
                    ShiftCost = CalculateShiftCost(allowances, awards)
                };

                shifts.Add(shiftDto);
            }

            return shifts;
        }

        public decimal CalculateShiftCost(List<AllowanceDto> allowances, List<AwardDto> awards)
        {
            decimal shiftCost = 0;

            foreach (AllowanceDto allowance in allowances)
            {
                shiftCost += allowance.AllowanceCost;
            }

            foreach (AwardDto award in awards)
            {
                shiftCost += award.AwardCost;
            }

            return shiftCost;
        }

        public void Save(List<ShiftDto> shiftDtos)
        {
            foreach (ShiftDto shiftDto in shiftDtos) { 
                
                Shift shift = ShiftMapper.MapDtoToModel(shiftDto);
                
                if(shiftsRepository.Get(shiftDto.ShiftId) == null)
                    shiftsRepository.Insert(shift);

                foreach (BreakDto breakDto in shiftDto.breakDtos)
                {
                    breakService.Save(breakDto, shiftDto);
                }

                foreach (AwardDto awardDto in shiftDto.awardDtos)
                {
                    awardService.Save(awardDto, shiftDto);
                }

                foreach(AllowanceDto allowanceDto in shiftDto.allowanceDtos)
                {
                    allowanceService.Save(allowanceDto, shiftDto);
                }
            }
        }
    }
}



















