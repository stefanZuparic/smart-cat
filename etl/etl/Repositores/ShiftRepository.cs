using etl.DTOs;
using etl.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etl.Repositores
{
    internal class ShiftRepository
    {
        PostgresContext dbContext = new PostgresContext();

        public List<Shift> GetAll()
        {
            return dbContext.Shifts.Include(s => s.Breaks)
                                   .Include( s => s.AwardInterpretations)
                                   .Include(s => s.Allowances)
                                   .ToList();
        }

        public Shift? Get(Guid id)
        {
            return dbContext.Shifts.Include(s => s.Breaks)
                                   .Include(s => s.AwardInterpretations)
                                   .Include(s => s.Allowances)
                                   .Where(s => s.ShiftId == id)
                                   .FirstOrDefault();
        }

        public void Insert(Shift shift)
        {
            dbContext.Shifts.Add(shift);
            dbContext.SaveChanges();
        }
    }
}
