using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class CarbonResult
{
    public int CarbonResultId { get; private set; }

    public double? TotalCarbonKg { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public bool? ValidationPassed { get; private set; }

    public virtual ICollection<LegCarbon> LegCarbons { get; private set; } = new List<LegCarbon>();
}
