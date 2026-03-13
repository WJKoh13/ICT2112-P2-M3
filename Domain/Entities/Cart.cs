using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Cart
{
    public int Cartid { get; private set; }

    public int? Customerid { get; private set; }

    public int? Sessionid { get; private set; }

    public DateTime? Rentalstart { get; private set; }

    public DateTime? Rentalend { get; private set; }

    public virtual ICollection<Cartitem> Cartitems { get; private set; } = new List<Cartitem>();

    public virtual ICollection<Checkout> Checkouts { get; private set; } = new List<Checkout>();

    public virtual Customer? Customer { get; private set; }

    public virtual Session? Session { get; private set; }
}
