using etl.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etl.Repositores
{
    internal class AwardRepository
    {
        PostgresContext dbContext = new PostgresContext();

        public AwardInterpretation? Get(Guid id)
        {
            return dbContext.AwardInterpretations.Include(a => a.Shift)
                                                 .Where(a => a.AwardId == id)
                                                 .FirstOrDefault();
        }

        public void Insert(AwardInterpretation award)
        {
            dbContext.AwardInterpretations.Add(award);
            dbContext.SaveChanges();
        }
    }
}
