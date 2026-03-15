using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class BatchOrder
{
    public int BatchId { get; private set; }

    public int OrderId { get; private set; }

    public DateTime? AddedTimestamp { get; private set; }

    public virtual DeliveryBatch Batch { get; private set; } = null!;

    public virtual Order Order { get; private set; } = null!;
}
