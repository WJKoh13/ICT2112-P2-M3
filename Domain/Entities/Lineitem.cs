using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Lineitem
{
    public int Lineitemid { get; private set; }

    public int? Requestid { get; private set; }

    public int? Productid { get; private set; }

    public int? Quantityrequest { get; private set; }

    public string? Remarks { get; private set; }

    public virtual Product? Product { get; private set; }

    public virtual Replenishmentrequest? Request { get; private set; }
}
