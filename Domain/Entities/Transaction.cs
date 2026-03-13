using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Transaction
{
    public int Transactionid { get; private set; }

    public int Orderid { get; private set; }

    public decimal Amount { get; private set; }

    public string? Providertransactionid { get; private set; }

    public DateTime Createdat { get; private set; }

    public virtual ICollection<Deposit> Deposits { get; private set; } = new List<Deposit>();

    public virtual Order Order { get; private set; } = null!;

    public virtual ICollection<Payment> Payments { get; private set; } = new List<Payment>();
}
