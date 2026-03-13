using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Plane
{
    public int TransportId { get; private set; }

    public int PlaneId { get; private set; }

    public string? PlaneType { get; private set; }

    public string? PlaneCallsign { get; private set; }

    public virtual Transport Transport { get; private set; } = null!;
}
