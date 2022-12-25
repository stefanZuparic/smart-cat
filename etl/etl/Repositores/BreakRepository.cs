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

        public List<Break> GetAll()
        {
            return dbContext.Breaks.ToList();
        }

        public Break Get(Guid id)
        {
            return dbContext.Breaks.Find(id);
        }

        public void Inser(Break br)
        {
            dbContext.Breaks.Add(br);
            dbContext.SaveChanges();
        }
    }
}
