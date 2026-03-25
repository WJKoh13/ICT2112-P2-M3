using ProRental.Domain.Module3.P2_1.Models;
using ProRental.Data.UnitOfWork;
using ProRental.Domain.Entities;
using ProRental.Interfaces.Module3.P2_1;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ProRental.Domain.Module3.P2_1.Controls;

public class InMemoryOrderService : IOrderService, IOrderSimulationService
{
    private readonly object _syncRoot = new();
    private readonly List<SimulatedOrder> _orders = [];
    private readonly IServiceScopeFactory _scopeFactory;

    public InMemoryOrderService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public SimulatedOrder CreateOrder(CreateOrderRequest request)
    {
        var sanitizedAddress = (request.DestinationAddress ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(sanitizedAddress))
        {
            throw new ArgumentException("Destination address is required.", nameof(request));
        }

        if (request.Quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.", nameof(request));
        }

        if (request.WeightKg <= 0)
        {
            throw new ArgumentException("Weight must be greater than zero.", nameof(request));
        }

        lock (_syncRoot)
        {
            var persistedOrderId = CreateOrderInDatabase(request);

            var order = new SimulatedOrder
            {
                OrderId = persistedOrderId,
                DestinationAddress = sanitizedAddress,
                Quantity = request.Quantity,
                WeightKg = request.WeightKg,
                UseBatchShipping = request.UseBatchShipping,
                IsDispatched = false,
                CreatedAtUtc = DateTime.UtcNow
            };

            _orders.Add(order);
            return order;
        }
    }

    private int CreateOrderInDatabase(CreateOrderRequest request)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var checkout = context.Checkouts
            .OrderBy(item => EF.Property<int>(item, "Checkoutid"))
            .FirstOrDefault();

        if (checkout is null)
        {
            throw new InvalidOperationException("No checkout records found. Seed or create checkout data before creating simulated orders.");
        }

        var checkoutId = checkout.ReadCheckoutId();
        var customerId = checkout.ReadCustomerId();
        var totalAmount = Convert.ToDecimal(request.Quantity * request.WeightKg);

        var order = Order.CreateSimulationOrder(customerId, checkoutId, totalAmount, DateTime.UtcNow);
        context.Orders.Add(order);
        context.SaveChanges();

        return order.ReadOrderId();
    }

    public List<SimulatedOrder> GetAllOrders()
    {
        lock (_syncRoot)
        {
            return _orders.OrderByDescending(order => order.CreatedAtUtc).ToList();
        }
    }

    public SimulatedOrder? FindById(int orderId)
    {
        lock (_syncRoot)
        {
            return _orders.FirstOrDefault(order => order.OrderId == orderId);
        }
    }

    public List<OrderItemDetail> OrderDetails(int orderId)
    {
        var order = FindById(orderId);
        if (order is null)
        {
            return [];
        }

        return
        [
            new OrderItemDetail
            {
                ProductId = "SIM-PRODUCT",
                Quantity = order.Quantity,
                WeightKg = order.WeightKg,
                IsDispatched = order.IsDispatched,
                UsesBatchShipping = order.UseBatchShipping
            }
        ];
    }

    public int GetOrderItemQuantity(int orderId, string productId)
    {
        return OrderDetails(orderId).FirstOrDefault(detail => detail.ProductId == productId)?.Quantity ?? 0;
    }

    public string GetDeliveryAddress(int orderId)
    {
        return FindById(orderId)?.DestinationAddress ?? string.Empty;
    }
}
