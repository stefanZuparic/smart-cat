using System;
using System.Collections.Generic;

namespace etl.Models;

public partial class Shift
{
    public Guid ShiftId { get; set; }

    public DateOnly ShiftDate { get; set; }

    public DateTime ShiftStart { get; set; }

    public DateTime ShiftFinish { get; set; }

    public decimal? ShiftCost { get; set; }

    public virtual ICollection<Allowance> Allowances { get; } = new List<Allowance>();

    public virtual ICollection<AwardInterpretation> AwardInterpretations { get; } = new List<AwardInterpretation>();

    public virtual ICollection<Break> Breaks { get; } = new List<Break>();
}
