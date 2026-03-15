using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Ship
{
    public int TransportId { get; private set; }

    public int ShipId { get; private set; }

    public string? VesselType { get; private set; }

    public string? VesselNumber { get; private set; }

    public string? MaxVesselSize { get; private set; }

    public virtual Transport Transport { get; private set; } = null!;
}
