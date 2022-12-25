using etl.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etl.Repositores
{
    internal class BreakRepository
    {
        PostgresContext dbContext = new PostgresContext();

        public List<Break> GetAll()
        {
            return dbContext.Breaks.Include(b => b.Shift)
                                   .ToList();
        }

        public Break? Get(Guid id)
        {
            return dbContext.Breaks.Include(a => a.Shift)
                                   .Where(a => a.BreakId == id)
                                   .FirstOrDefault();
        }

        public void Insert(Break br)
        {
            dbContext.Breaks.Add(br);
            dbContext.SaveChanges();
        }
    }
}
