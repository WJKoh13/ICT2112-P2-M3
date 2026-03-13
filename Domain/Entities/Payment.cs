using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Payment
{
    public string Paymentid { get; private set; } = null!;

    public int Orderid { get; private set; }

    public int Transactionid { get; private set; }

    public decimal Amount { get; private set; }

    public DateTime Createdat { get; private set; }

    public virtual Order Order { get; private set; } = null!;

    public virtual Transaction Transaction { get; private set; } = null!;
}
