using System;
using System.Collections.Generic;

namespace etl.Models;

public partial class Break
{
    public Guid BreakId { get; set; }

    public Guid ShiftId { get; set; }

    public DateTime BreakStart { get; set; }

    public DateTime BreakFinish { get; set; }

    public bool? IsPaid { get; set; }

    public virtual Shift Shift { get; set; } = null!;
}
