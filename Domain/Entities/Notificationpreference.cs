using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Notificationpreference
{
    public int Preferenceid { get; private set; }

    public int Userid { get; private set; }

    public bool Emailenabled { get; private set; }

    public bool Smsenabled { get; private set; }

    public virtual User User { get; private set; } = null!;
}
