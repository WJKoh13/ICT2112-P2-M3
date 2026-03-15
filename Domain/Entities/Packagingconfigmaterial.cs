using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Packagingconfigmaterial
{
    public int Configurationid { get; private set; }

    public int Materialid { get; private set; }

    public string? Category { get; private set; }

    public int Quantity { get; private set; }

    public virtual Packagingconfiguration Configuration { get; private set; } = null!;

    public virtual Packagingmaterial Material { get; private set; } = null!;
}
