using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Customerreward
{
    public int Customerrewardsid { get; private set; }

    public int Customerid { get; private set; }

    public double Discount { get; private set; }

    public double Totalcarbon { get; private set; }

    public virtual Customer Customer { get; private set; } = null!;
}
