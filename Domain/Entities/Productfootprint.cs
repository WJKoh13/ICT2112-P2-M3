using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Productfootprint
{
    public int Productcarbonfootprintid { get; private set; }

    public int Productid { get; private set; }

    public int Badgeid { get; private set; }

    public double? Producttoxicpercentage { get; private set; }

    public double Totalco2 { get; private set; }

    public DateTime Calculatedat { get; private set; }

    public virtual Ecobadge Badge { get; private set; } = null!;

    public virtual Product Product { get; private set; } = null!;
}
