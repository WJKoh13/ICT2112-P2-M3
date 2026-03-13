using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Stafffootprint
{
    public int Staffcarbonfootprintid { get; private set; }

    public int Staffid { get; private set; }

    public DateTime Time { get; private set; }

    public double Hoursworked { get; private set; }

    public double Totalstaffco2 { get; private set; }

    public virtual Staff Staff { get; private set; } = null!;
}
