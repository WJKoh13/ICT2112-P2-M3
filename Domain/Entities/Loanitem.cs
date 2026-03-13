using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Loanitem
{
    public int Loanitemid { get; private set; }

    public int Loanlistid { get; private set; }

    public int Inventoryitemid { get; private set; }

    public string? Remarks { get; private set; }

    public virtual Inventoryitem Inventoryitem { get; private set; } = null!;

    public virtual Loanlist Loanlist { get; private set; } = null!;
}
