using etl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etl.Repositores
{
    internal class KpiRepository
    {
        PostgresContext dbContext = new PostgresContext();

        public List<Kpi> GetAll()
        {
            return dbContext.Kpis.ToList();
        }

        public void Save(Kpi kpi)
        {
            dbContext.Kpis.Add(kpi);
            dbContext.SaveChanges();
        }

        public Kpi GetSpecificKpi(string name, DateOnly date)
        {
            try
            {
                return dbContext.Kpis.Where(k => k.KpiName == name && k.KpiDate == date).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
    }
}
