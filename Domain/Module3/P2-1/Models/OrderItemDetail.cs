namespace ProRental.Domain.Module3.P2_1.Models;

public class OrderItemDetail
{
    public required string ProductId { get; set; }
    public int Quantity { get; set; }
    public double WeightKg { get; set; }
    public bool IsDispatched { get; set; }
    public bool UsesBatchShipping { get; set; }
}
