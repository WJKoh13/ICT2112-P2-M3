using ProRental.Domain.Enums;

namespace ProRental.Domain.Entities;

public partial class Transport
{
    private TransportMode? _transportMode;
    private TransportMode? TransportMode { get => _transportMode; set => _transportMode = value; }

    public int ReadTransportId() => _transportId;

    public string ReadTransportationType() => (_transportMode ?? default).ToString();

    public float ReadMaxLoadKg() => (float)(_maxLoadKg ?? 0d);

    public float ReadVehicleSizeM2() => (float)(_vehicleSizeM2 ?? 0d);

    public bool ReadIsAvailable() => _isAvailable ?? false;
}
