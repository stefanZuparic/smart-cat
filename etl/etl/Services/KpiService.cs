using etl.Models;
using etl.Repositores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etl.Services
{
    internal class KpiService
    {
        KpiRepository kpiRepository = new KpiRepository();

        BreakService breakService = new BreakService();
        ShiftService shiftService = new ShiftService();
        AllowanceService allowanceService = new AllowanceService();
        AwardService awardService = new AwardService();

        public void CalculateKpis()
        {
            TotalNumberOfPaidBreaks();
            MinShiftLengthInHours();
            MeanShiftCost();
            MeanBreakLengthInMinutes();
            MaxBreakFreeShiftPeriodInDays();
            MaxAllowanceCost14d();
        }

        public void TotalNumberOfPaidBreaks()
        {
            Save("total_number_of_paid_breaks", breakService.TotalNumberOfPaidBreaks());
        }

        public void MinShiftLengthInHours()
        {
            Save("min_shift_length_in_hours", shiftService.MinShiftLengthInHours());
        }

        public void MeanShiftCost()
        {
            Save("mean_shift_cost", shiftService.MeanShiftCost());
        }

        public void MeanBreakLengthInMinutes()
        {
            Save("mean_break_length_in_minutes", breakService.MeanBreakLengthInMinutes());
        }

        public void MaxBreakFreeShiftPeriodInDays() 
        {
            Save("max_break_free_shift_period_in_days", shiftService.MaxBreakFreeShiftPeriodInDays());
        }

        public void MaxAllowanceCost14d()
        {
            Save("max_allowance_cost_14d", allowanceService.MaxAllowanceCost14Days());
        }


        public void Save(string name, decimal value) 
        {
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);

            Kpi? existing = kpiRepository.GetKpiByNameAndDate(name, date);

            if (existing == null)
            {
                Kpi kpi = new Kpi()
                {
                    KpiName = name,
                    KpiDate = date,
                    KpiValue = value
                };

                kpiRepository.Insert(kpi);
            }
            else 
            {
                existing.KpiValue = value;
                kpiRepository.Update(existing);
            }
        }

    }
}
