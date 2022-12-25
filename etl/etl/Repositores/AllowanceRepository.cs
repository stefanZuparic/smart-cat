using etl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etl.Repositores
{
    internal class AllowanceRepository
    {
        PostgresContext dbContext = new PostgresContext();

        public Allowance Get(Guid id)
        {
            return dbContext.Allowances.Find(id);
        }

        public void Inser(Allowance allowance)
        {
            dbContext.Allowances.Add(allowance);
            dbContext.SaveChanges();
        }
    }
}
