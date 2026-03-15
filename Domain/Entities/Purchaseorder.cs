using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Purchaseorder
{
    public int Poid { get; private set; }

    public int? Supplierid { get; private set; }

    public DateOnly? Podate { get; private set; }

    public DateOnly? Expecteddeliverydate { get; private set; }

    public decimal? Totalamount { get; private set; }

    public virtual ICollection<Polineitem> Polineitems { get; private set; } = new List<Polineitem>();

    public virtual ICollection<Purchaseorderlog> Purchaseorderlogs { get; private set; } = new List<Purchaseorderlog>();
}
