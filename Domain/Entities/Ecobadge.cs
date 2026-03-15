using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Ecobadge
{
    public int Badgeid { get; private set; }

    public double Maxcarbong { get; private set; }

    public string? Criteriadescription { get; private set; }

    public string Badgename { get; private set; } = null!;

    public virtual ICollection<Productfootprint> Productfootprints { get; private set; } = new List<Productfootprint>();
}
