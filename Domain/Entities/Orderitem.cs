using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Orderitem
{
    public int Orderitemid { get; private set; }

    public int Orderid { get; private set; }

    public int Productid { get; private set; }

    public int Quantity { get; private set; }

    public decimal Unitprice { get; private set; }

    public DateTime? Rentalstartdate { get; private set; }

    public DateTime? Rentalenddate { get; private set; }

    public virtual Order Order { get; private set; } = null!;

    public virtual Product Product { get; private set; } = null!;
}
