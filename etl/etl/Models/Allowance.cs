using System;
using System.Collections.Generic;

namespace etl.Models;

public partial class Allowance
{
    public Guid AllowanceId { get; set; }

    public Guid ShiftId { get; set; }

    public double AllowanceValue { get; set; }

    public decimal AllowanceCost { get; set; }

    public virtual Shift Shift { get; set; } = null!;
}
