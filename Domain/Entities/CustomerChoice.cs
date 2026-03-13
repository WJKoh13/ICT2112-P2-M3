using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class CustomerChoice
{
    public int CustomerId { get; private set; }

    public int OrderId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public virtual Customer Customer { get; private set; } = null!;

    public virtual Order Order { get; private set; } = null!;
}
