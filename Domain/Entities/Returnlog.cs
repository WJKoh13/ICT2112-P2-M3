using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Returnlog
{
    public int Returnlogid { get; private set; }

    public int Returnrequestid { get; private set; }

    public int Rentalorderlogid { get; private set; }

    public string? Customerid { get; private set; }

    public DateTime? Requestdate { get; private set; }

    public DateTime? Completiondate { get; private set; }

    public string? Imageurl { get; private set; }

    public string? Detailsjson { get; private set; }

    public virtual Rentalorderlog Rentalorderlog { get; private set; } = null!;

    public virtual Transactionlog ReturnlogNavigation { get; private set; } = null!;

    public virtual Returnrequest Returnrequest { get; private set; } = null!;
}
