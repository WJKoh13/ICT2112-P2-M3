using ProRental.Domain.Enums;

namespace ProRental.Domain.Entities;

public partial class PricingRule
{
    private TransportMode? _transportMode;
    private TransportMode? TransportMode { get => _transportMode; set => _transportMode = value; }

    public int ReadRuleId() => _ruleId;

    public TransportMode? ReadTransportMode() => _transportMode;

    public decimal? ReadBaseRatePerKm() => _baseRatePerKm;

    public bool ReadIsActive() => _isActive ?? false;

    public decimal? ReadCarbonSurcharge() => _carbonSurcharge;
}
