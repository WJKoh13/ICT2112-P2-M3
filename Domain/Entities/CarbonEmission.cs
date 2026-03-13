using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class CarbonEmission
{
    public int EmissionId { get; private set; }

    public int StageId { get; private set; }

    public double? CarbonKg { get; private set; }

    public virtual ReturnStage Stage { get; private set; } = null!;
}
