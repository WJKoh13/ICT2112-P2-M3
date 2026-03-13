using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Productdetail
{
    public int Detailsid { get; private set; }

    public int Productid { get; private set; }

    public int Totalquantity { get; private set; }

    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public decimal? Weight { get; private set; }

    public string? Image { get; private set; }

    public decimal Price { get; private set; }

    public decimal? Depositrate { get; private set; }

    public virtual Product Product { get; private set; } = null!;
}
