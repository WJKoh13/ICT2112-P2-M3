namespace ProRental.Domain.Module3.P2_1.Models;

public class SimulatedOrder
{
    public int OrderId { get; set; }
    public string DestinationAddress { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public double WeightKg { get; set; }
    public bool UseBatchShipping { get; set; }
    public bool IsDispatched { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}
