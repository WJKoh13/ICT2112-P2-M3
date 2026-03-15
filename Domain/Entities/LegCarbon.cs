using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class LegCarbon
{
    public int LegId { get; private set; }

    public double? DistanceKm { get; private set; }

    public double? WeightKg { get; private set; }

    public double? CarbonKg { get; private set; }

    public double? CarbonRate { get; private set; }

    public int? CarbonResultId { get; private set; }

    public int? RouteLegId { get; private set; }

    public virtual CarbonResult? CarbonResult { get; private set; }

    public virtual RouteLeg? RouteLeg { get; private set; }
}
