using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Packagingmaterial
{
    public int Materialid { get; private set; }

    public string Name { get; private set; } = null!;

    public string? Type { get; private set; }

    public bool Recyclable { get; private set; }

    public bool Reusable { get; private set; }

    public virtual ICollection<Packagingconfigmaterial> Packagingconfigmaterials { get; private set; } = new List<Packagingconfigmaterial>();
}
