using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Rentalorderlog
{
    public int Rentalorderlogid { get; private set; }

    public int? Orderid { get; private set; }

    public int? Customerid { get; private set; }

    public DateTime? Orderdate { get; private set; }

    public decimal? Totalamount { get; private set; }

    public string? Detailsjson { get; private set; }

    public virtual ICollection<Loanlog> Loanlogs { get; private set; } = new List<Loanlog>();

    public virtual Order? Order { get; private set; }

    public virtual Transactionlog RentalorderlogNavigation { get; private set; } = null!;

    public virtual ICollection<Returnlog> Returnlogs { get; private set; } = new List<Returnlog>();
}
