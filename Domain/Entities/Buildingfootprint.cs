using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Buildingfootprint
{
    public int Buildingcarbonfootprintid { get; private set; }

    public DateTime Timehourly { get; private set; }

    public string? Zone { get; private set; }

    public string? Block { get; private set; }

    public string? Floor { get; private set; }

    public string? Room { get; private set; }

    public double Totalroomco2 { get; private set; }
}
