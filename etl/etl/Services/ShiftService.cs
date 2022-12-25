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
    internal class ShiftService
    {
        ShiftRepository shiftsRepository = new ShiftRepository();
        
        BreakService breakService = new BreakService();
        AllowanceService allowanceService = new AllowanceService();
        AwardService awardService = new AwardService();

        public async Task<List<ShiftDTO>> LoadShifts()
        { 
            List<ShiftDTO> shifts = new List<ShiftDTO>();

            string baseUrl = "http://localhost:8000";
            string nextUrl = "/api/shifts?start=0&limit=30";

            while (nextUrl != null) {
                using (HttpResponseMessage response = await ApiHelper.ShiftsAPI.GetAsync(baseUrl + nextUrl))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonRawData = JObject.Parse(await response.Content.ReadAsStringAsync());

                        ConvertJsonToShiftsDTO(jsonRawData, ref shifts);

                        if (jsonRawData["links"]["next"] != null)
                            nextUrl = jsonRawData["links"]["next"].ToString();
                        else
                            nextUrl = null;

                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
            }

            return shifts;
        }

        public List<ShiftDTO> ConvertJsonToShiftsDTO(JObject JsonRawData, ref List<ShiftDTO> shifts)
        {

            foreach (var shift in JsonRawData["results"]) 
            {
                List<BreakDTO> breaks = breakService.ConvertJsonToBreakDTO(shift);   
                List<AwardDTO> awards = awardService.ConvertJsonToAwardDTO(shift);
                List<AllowanceDTO> allowances = allowanceService.ConvertJsonToAllowanceDTO(shift);

                if (Guid.TryParse(shift["id"].ToString(), out Guid id) &&
                    DateOnly.TryParse(shift["date"].ToString(), out DateOnly date) &&
                    long.TryParse(shift["start"].ToString(), out long start) &&
                    long.TryParse(shift["finish"].ToString(), out long finish)
                    )
                {
                    ShiftDTO shiftDTO = new ShiftDTO()
                    {
                        ShiftId = id,
                        ShiftDate = date,
                        ShiftStart = DateTimeOffset.FromUnixTimeMilliseconds(start).DateTime,
                        ShiftFinish = DateTimeOffset.FromUnixTimeMilliseconds(finish).DateTime,
                        breakDTOs = breaks,
                        allowanceDTOs = allowances,
                        awardDTOs = awards,
                        ShiftCost = CalculateShiftCost(allowances, awards)
                    };

                    shifts.Add(shiftDTO);
                }
            }

            return shifts;
        }

        public decimal CalculateShiftCost(List<AllowanceDTO> allowances, List<AwardDTO> awards)
        {
            decimal shiftCost = 0;

            foreach (AllowanceDTO allowance in allowances)
            {
                shiftCost += allowance.AllowanceCost;
            }

            foreach (AwardDTO award in awards)
            {
                shiftCost += award.AwardCost;
            }

            return shiftCost;
        }

        public void Save(List<ShiftDTO> shiftDTOs)
        {
            foreach (ShiftDTO shiftDTO in shiftDTOs) { 
                
                Shift shift = ShiftMapper.MapDTOToModel(shiftDTO);

                Shift? existing = shiftsRepository.Get(shiftDTO.ShiftId);

                if (existing == null)
                    shiftsRepository.Insert(shift);

                foreach (BreakDTO breakDTO in shiftDTO.breakDTOs)
                {
                    breakService.Save(breakDTO, shiftDTO);
                }

                foreach (AwardDTO awardDTO in shiftDTO.awardDTOs)
                {
                    awardService.Save(awardDTO, shiftDTO);
                }

                foreach(AllowanceDTO allowanceDTO in shiftDTO.allowanceDTOs)
                {
                    allowanceService.Save(allowanceDTO, shiftDTO);
                }
            }
        }

        public decimal MinShiftLengthInHours()
        {
            List<Shift> shifts = shiftsRepository.GetAll();

            List<decimal> shiftLenth = new List<decimal>();

            if (shifts.Count > 0)
            {
                foreach (Shift shift in shifts)
                {
                    shiftLenth.Add(shift.ShiftFinish.Hour - shift.ShiftStart.Hour);
                }

                return shiftLenth.Min();
            }

            return 0;
        }

        public decimal MeanShiftCost()
        {
            List<Shift> shifts = shiftsRepository.GetAll();
            decimal? cost = 0;

            if (shifts.Count > 0)
            {
                foreach (Shift shift in shifts)
                {
                    cost += shift.ShiftCost;
                }

                return (decimal)(cost / shifts.Count);
            }

            return 0;
        }

        public decimal MaxBreakFreeShiftPeriodInDays()
        {
            List<Shift> shifts = shiftsRepository.GetAll().OrderBy(s => s.ShiftDate).ToList();
            decimal max = 0, count = 0;

            for (int i = 0; i < shifts.Count; i++)
            {
                if (shifts[i].Breaks.Count == 0)
                    count++;
                else
                {
                    if (count > max)
                        max = count;

                    count = 0;
                }
            }
            return max;
        }
    }
}



















