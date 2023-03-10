using etl.Models;
using Microsoft.EntityFrameworkCore;
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

        public List<Allowance> GetAll()
        { 
            return dbContext.Allowances.Include(a => a.Shift).ToList();
        }

        public Allowance? Get(Guid id)
        {
            return dbContext.Allowances.Include(a => a.Shift)
                                       .Where(a => a.AllowanceId == id)
                                       .FirstOrDefault();
        }

        public void Insert(Allowance allowance)
        {
            dbContext.Allowances.Add(allowance);
            dbContext.SaveChanges();
        }
    }
}
