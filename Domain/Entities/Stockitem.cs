using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Stockitem
{
    public int Productid { get; private set; }

    public string? Sku { get; private set; }

    public string? Name { get; private set; }

    public string? Uom { get; private set; }

    public virtual ICollection<Polineitem> Polineitems { get; private set; } = new List<Polineitem>();
}
