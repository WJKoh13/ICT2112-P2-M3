namespace ProRental.Domain.Module3.P2_1.Models;

public class CreateOrderRequest
{
    public string DestinationAddress { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public double WeightKg { get; set; }
    public bool UseBatchShipping { get; set; }
}
