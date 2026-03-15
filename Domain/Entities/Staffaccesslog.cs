using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Staffaccesslog
{
    public int Accessid { get; private set; }

    public int Staffid { get; private set; }

    public DateTime Eventtime { get; private set; }

    public virtual Staff Staff { get; private set; } = null!;
}
