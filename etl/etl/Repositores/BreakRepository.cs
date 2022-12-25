using etl.Models;
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

        public Break Get(Guid id)
        {
            try
            {
                return dbContext.Breaks.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Inser(Break br)
        {
            dbContext.Breaks.Add(br);
            dbContext.SaveChanges();
        }
    }
}
