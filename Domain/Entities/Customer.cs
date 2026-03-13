using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Customer
{
    public int Customerid { get; private set; }

    public int Userid { get; private set; }

    public string Address { get; private set; } = null!;

    public int Customertype { get; private set; }

    public virtual ICollection<Cart> Carts { get; private set; } = new List<Cart>();

    public virtual ICollection<Checkout> Checkouts { get; private set; } = new List<Checkout>();

    public virtual ICollection<CustomerChoice> CustomerChoices { get; private set; } = new List<CustomerChoice>();

    public virtual ICollection<Customerreward> Customerrewards { get; private set; } = new List<Customerreward>();

    public virtual ICollection<Loanlist> Loanlists { get; private set; } = new List<Loanlist>();

    public virtual ICollection<Order> Orders { get; private set; } = new List<Order>();

    public virtual ICollection<Refund> Refunds { get; private set; } = new List<Refund>();

    public virtual ICollection<Returnrequest> Returnrequests { get; private set; } = new List<Returnrequest>();

    public virtual User User { get; private set; } = null!;
}
