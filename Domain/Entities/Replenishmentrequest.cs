using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Replenishmentrequest
{
    public int Requestid { get; private set; }

    public string? Requestedby { get; private set; }

    public DateTime? Createdat { get; private set; }

    public string? Remarks { get; private set; }

    public DateTime? Completedat { get; private set; }

    public string? Completedby { get; private set; }

    public virtual ICollection<Lineitem> Lineitems { get; private set; } = new List<Lineitem>();
}
