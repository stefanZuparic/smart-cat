using etl.Models;
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

        public void Inser(AwardInterpretation award)
        {
            dbContext.AwardInterpretations.Add(award);
            dbContext.SaveChanges();
        }
    }
}
