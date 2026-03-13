using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Reliabilityrating
{
    public int Ratingid { get; private set; }

    public int? Supplierid { get; private set; }

    public decimal? Score { get; private set; }

    public string? Rationale { get; private set; }

    public int? Calculatedbyuserid { get; private set; }

    public DateTime? Calculatedat { get; private set; }

    public virtual Supplier? Supplier { get; private set; }

    public virtual ICollection<Vettingrecord> Vettingrecords { get; private set; } = new List<Vettingrecord>();
}
