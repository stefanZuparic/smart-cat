using System;
using System.Collections.Generic;

namespace etl.Models;

public partial class Kpi
{
    public int KpiId { get; set; }

    public string KpiName { get; set; } = null!;

    public DateOnly KpiDate { get; set; }

    public decimal KpiValue { get; set; }
}
