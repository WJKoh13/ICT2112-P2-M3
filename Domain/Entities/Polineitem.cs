using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Polineitem
{
    public int Polineid { get; private set; }

    public int? Poid { get; private set; }

    public int? Productid { get; private set; }

    public int? Qty { get; private set; }

    public decimal? Unitprice { get; private set; }

    public decimal? Linetotal { get; private set; }

    public virtual Purchaseorder? Po { get; private set; }

    public virtual Stockitem? Product { get; private set; }
}
