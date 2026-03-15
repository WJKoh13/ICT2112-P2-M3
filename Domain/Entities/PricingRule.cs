using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class PricingRule
{
    public int RuleId { get; private set; }

    public decimal? BaseRatePerKm { get; private set; }

    public bool? IsActive { get; private set; }

    public decimal? CarbonSurcharge { get; private set; }
}
