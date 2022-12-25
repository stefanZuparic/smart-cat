using etl.DTOs;
using etl.Helpers;
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
                List<BreakDto> breaks = new List<BreakDto>();   
                List<AwardDto> awards = new List<AwardDto>();
                List<AllowanceDto> allowances = new List<AllowanceDto>();

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

                foreach (var award in shift["award_interpretations"])
                {
                    AwardDto awardDto = new AwardDto()
                    {
                        AwardId = (Guid)award["id"],
                        AwardDate  = DateOnly.Parse(award["date"].ToString()),
                        AwardUnits = double.Parse(award["units"].ToString()),
                        AwardCost = decimal.Parse(award["cost"].ToString())
                    };

                    awards.Add(awardDto);
                };

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
    }
}



















