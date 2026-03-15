using System;
using System.Collections.Generic;

namespace ProRental.Domain.Entities;

public partial class Shipment
{
    public int Trackingid { get; private set; }

    public int Orderid { get; private set; }

    public int Batchid { get; private set; }

    public double Weight { get; private set; }

    public string Destination { get; private set; } = null!;

    public virtual DeliveryBatch Batch { get; private set; } = null!;

    public virtual Order Order { get; private set; } = null!;
}
