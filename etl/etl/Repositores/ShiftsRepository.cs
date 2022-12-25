using etl.DTOs;
using etl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etl.Repositores
{
    internal class ShiftsRepository
    {
        PostgresContext dbContext = new PostgresContext();

        public void Inser(Shift shift)
        {
            dbContext.Shifts.Add(shift);
            dbContext.SaveChanges();
        }
    }
}
