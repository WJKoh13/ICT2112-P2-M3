using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Session
{
    public int Sessionid { get; private set; }

    public int Userid { get; private set; }

    public string Role { get; private set; } = null!;

    public DateTime Createdat { get; private set; }

    public DateTime Expiresat { get; private set; }

    public virtual ICollection<Cart> Carts { get; private set; } = new List<Cart>();

    public virtual User User { get; private set; } = null!;
}
