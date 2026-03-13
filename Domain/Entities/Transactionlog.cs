using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Transactionlog
{
    public int Transactionlogid { get; private set; }

    public DateTime? Createdat { get; private set; }

    public virtual Clearancelog? Clearancelog { get; private set; }

    public virtual Loanlog? Loanlog { get; private set; }

    public virtual Purchaseorderlog? Purchaseorderlog { get; private set; }

    public virtual Rentalorderlog? Rentalorderlog { get; private set; }

    public virtual Returnlog? Returnlog { get; private set; }

    public virtual ICollection<Analytic> Analytics { get; private set; } = new List<Analytic>();
}
