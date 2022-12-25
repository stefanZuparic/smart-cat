using System;
using System.Collections.Generic;

namespace etl.Models;

public partial class AwardInterpretation
{
    public Guid AwardId { get; set; }

    public Guid ShiftId { get; set; }

    public DateOnly AwardDate { get; set; }

    public double AwardUnits { get; set; }

    public decimal AwardCost { get; set; }

    public virtual Shift Shift { get; set; } = null!;
}
