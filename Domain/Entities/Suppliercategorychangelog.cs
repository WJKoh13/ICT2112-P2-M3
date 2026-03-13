using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Suppliercategorychangelog
{
    public int Logid { get; private set; }

    public int? Supplierid { get; private set; }

    public string? Changereason { get; private set; }

    public DateTime? Changedat { get; private set; }

    public virtual Supplier? Supplier { get; private set; }
}
