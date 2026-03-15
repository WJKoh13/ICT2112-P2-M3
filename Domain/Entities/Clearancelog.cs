using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Clearancelog
{
    public int Clearancelogid { get; private set; }

    public int Clearancebatchid { get; private set; }

    public string? Batchname { get; private set; }

    public DateTime? Clearancedate { get; private set; }

    public string? Detailsjson { get; private set; }

    public virtual Clearancebatch Clearancebatch { get; private set; } = null!;

    public virtual Transactionlog ClearancelogNavigation { get; private set; } = null!;
}
