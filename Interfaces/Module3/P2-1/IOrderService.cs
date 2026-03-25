using ProRental.Domain.Module3.P2_1.Models;

namespace ProRental.Interfaces.Module3.P2_1;

public interface IOrderService
{
    List<OrderItemDetail> OrderDetails(int orderId);
    int GetOrderItemQuantity(int orderId, string productId);
    string GetDeliveryAddress(int orderId);
}
