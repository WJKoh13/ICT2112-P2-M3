// This interface is owned by Module 2 (P2-2).
// P2-5 depends on it to retrieve order and product details for packaging profile creation.

namespace ProRental.Data.Module2.Interfaces;

public class OrderProductInfo
{
    public int OrderId { get; set; }
    public string ProductName { get; set; } = null!;
    public float Weight { get; set; }
}

public interface IOrderService
{
    List<OrderProductInfo> GetAllOrders();
}
