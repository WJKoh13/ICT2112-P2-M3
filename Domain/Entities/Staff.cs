using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Staff
{
    public int Staffid { get; private set; }

    public int Userid { get; private set; }

    public string Department { get; private set; } = null!;

    public virtual ICollection<Staffaccesslog> Staffaccesslogs { get; private set; } = new List<Staffaccesslog>();

    public virtual ICollection<Stafffootprint> Stafffootprints { get; private set; } = new List<Stafffootprint>();

    public virtual User User { get; private set; } = null!;
}
