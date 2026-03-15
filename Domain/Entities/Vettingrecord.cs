using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Vettingrecord
{
    public int Vettingid { get; private set; }

    public int? Ratingid { get; private set; }

    public int? Supplierid { get; private set; }

    public int? Vettedbyuserid { get; private set; }

    public DateTime? Vettedat { get; private set; }

    public string? Notes { get; private set; }

    public virtual Reliabilityrating? Rating { get; private set; }

    public virtual Supplier? Supplier { get; private set; }
}
