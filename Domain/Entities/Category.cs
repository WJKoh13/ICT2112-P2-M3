using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Category
{
    public int Categoryid { get; private set; }

    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public DateTime Createddate { get; private set; }

    public DateTime Updateddate { get; private set; }

    public virtual ICollection<Product> Products { get; private set; } = new List<Product>();
}
