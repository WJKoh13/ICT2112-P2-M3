using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Packagingprofile
{
    public int Profileid { get; private set; }

    public int Orderid { get; private set; }

    public double Volume { get; private set; }

    public string? Fragilitylevel { get; private set; }

    public virtual Order Order { get; private set; } = null!;

    public virtual ICollection<Packagingconfiguration> Packagingconfigurations { get; private set; } = new List<Packagingconfiguration>();
}
