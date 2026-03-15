using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Reportexport
{
    public int Reportid { get; private set; }

    public int? Refanalyticsid { get; private set; }

    public string? Title { get; private set; }

    public string? Url { get; private set; }

    public virtual Analytic? Refanalytics { get; private set; }
}
