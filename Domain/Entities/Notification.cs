using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Notification
{
    public int Notificationid { get; private set; }

    public int Userid { get; private set; }

    public string Message { get; private set; } = null!;

    public DateTime Datesent { get; private set; }

    public bool Isread { get; private set; }

    public virtual User User { get; private set; } = null!;
}
