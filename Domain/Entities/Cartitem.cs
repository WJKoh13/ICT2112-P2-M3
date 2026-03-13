using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Cartitem
{
    public int Cartitemid { get; private set; }

    public int Cartid { get; private set; }

    public int Productid { get; private set; }

    public int Quantity { get; private set; }

    public bool? Isselected { get; private set; }

    public virtual Cart Cart { get; private set; } = null!;

    public virtual Product Product { get; private set; } = null!;
}
