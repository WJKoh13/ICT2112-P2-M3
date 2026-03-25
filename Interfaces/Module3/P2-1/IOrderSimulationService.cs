using ProRental.Domain.Module3.P2_1.Models;

namespace ProRental.Interfaces.Module3.P2_1;

public interface IOrderSimulationService
{
    SimulatedOrder CreateOrder(CreateOrderRequest request);
    List<SimulatedOrder> GetAllOrders();
    SimulatedOrder? FindById(int orderId);
}