using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Damagereport
{
    public int Damagereportid { get; private set; }

    public int Returnitemid { get; private set; }

    public string? Description { get; private set; }

    public string? Severity { get; private set; }

    public decimal? Repaircost { get; private set; }

    public string? Images { get; private set; }

    public DateTime Reportdate { get; private set; }

    public virtual Returnitem Returnitem { get; private set; } = null!;
}
