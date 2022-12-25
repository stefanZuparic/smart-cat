using etl.Models;
using etl.Repositores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etl.Services
{
    internal class KpisService
    {
        KpiRepository kpisRepository = new KpiRepository();
        BreakService breakService = new BreakService();


        public void TotalNumberOfPaidBreaks()
        { 
            int count = breakService.TotalNumberOfPaidBreaks();
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);

            if (kpisRepository.GetSpecificKpi("total_number_of_paid_breaks", date) == null)
            {
                Kpi kpi = new Kpi() {
                    KpiName = "total_number_of_paid_breaks",
                    KpiDate = date,
                    KpiValue = count
                };

                kpisRepository.Save(kpi);
            }
        }


    }
}
