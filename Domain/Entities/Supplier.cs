using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Supplier
{
    public int Supplierid { get; private set; }

    public string? Name { get; private set; }

    public string? Details { get; private set; }

    public int? Creditperiod { get; private set; }

    public double? Avgturnaroundtime { get; private set; }

    public bool? Isverified { get; private set; }

    public virtual ICollection<Reliabilityrating> Reliabilityratings { get; private set; } = new List<Reliabilityrating>();

    public virtual ICollection<Suppliercategorychangelog> Suppliercategorychangelogs { get; private set; } = new List<Suppliercategorychangelog>();

    public virtual ICollection<Vettingrecord> Vettingrecords { get; private set; } = new List<Vettingrecord>();
}
