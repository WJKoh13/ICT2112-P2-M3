using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Truck
{
    public int TransportId { get; private set; }

    public int TruckId { get; private set; }

    public string? TruckType { get; private set; }

    public string? LicensePlate { get; private set; }

    public virtual Transport Transport { get; private set; } = null!;
}
