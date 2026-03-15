using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Loanlog
{
    public int Loanlogid { get; private set; }

    public int Loanlistid { get; private set; }

    public int Rentalorderlogid { get; private set; }

    public DateTime? Loandate { get; private set; }

    public DateTime? Returndate { get; private set; }

    public DateTime? Duedate { get; private set; }

    public string? Detailsjson { get; private set; }

    public virtual Loanlist Loanlist { get; private set; } = null!;

    public virtual Transactionlog LoanlogNavigation { get; private set; } = null!;

    public virtual Rentalorderlog Rentalorderlog { get; private set; } = null!;
}
