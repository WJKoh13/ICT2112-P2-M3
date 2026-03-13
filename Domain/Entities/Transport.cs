using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Transport
{
    public int TransportId { get; private set; }

    public double? MaxLoadKg { get; private set; }

    public double? VehicleSizeM2 { get; private set; }

    public bool? IsAvailable { get; private set; }

    public virtual Plane? Plane { get; private set; }

    public virtual ICollection<RouteLeg> RouteLegs { get; private set; } = new List<RouteLeg>();

    public virtual Ship? Ship { get; private set; }

    public virtual Train? Train { get; private set; }

    public virtual Truck? Truck { get; private set; }
}
