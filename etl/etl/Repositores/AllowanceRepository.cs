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

        public void Inser(Allowance allowance)
        {
            dbContext.Allowances.Add(allowance);
            dbContext.SaveChanges();
        }
    }
}
