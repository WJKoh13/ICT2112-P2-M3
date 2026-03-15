using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Purchaseorderlog
{
    public int Purchaseorderlogid { get; private set; }

    public int Poid { get; private set; }

    public DateTime? Podate { get; private set; }

    public int? Supplierid { get; private set; }

    public DateTime? Expecteddeliverydate { get; private set; }

    public decimal? Totalamount { get; private set; }

    public string? Detailsjson { get; private set; }

    public virtual Purchaseorder Po { get; private set; } = null!;

    public virtual Transactionlog PurchaseorderlogNavigation { get; private set; } = null!;
}
