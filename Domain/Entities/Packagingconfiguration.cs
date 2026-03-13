using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Packagingconfiguration
{
    public int Configurationid { get; private set; }

    public int Profileid { get; private set; }

    public virtual ICollection<Packagingconfigmaterial> Packagingconfigmaterials { get; private set; } = new List<Packagingconfigmaterial>();

    public virtual Packagingprofile Profile { get; private set; } = null!;
}
