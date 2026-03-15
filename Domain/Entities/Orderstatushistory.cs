using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Orderstatushistory
{
    public int Historyid { get; private set; }

    public int Orderid { get; private set; }

    public DateTime Timestamp { get; private set; }

    public string Updatedby { get; private set; } = null!;

    public string? Remark { get; private set; }

    public virtual Order Order { get; private set; } = null!;
}
