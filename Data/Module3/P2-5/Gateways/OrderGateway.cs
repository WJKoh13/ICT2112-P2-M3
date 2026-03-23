using ProRental.Data.UnitOfWork;
using ProRental.Domain.Entities;
using ProRental.Interfaces.Data;

namespace ProRental.Data;

public class OrderGateway : IOrderGateway
{
    private readonly AppDbContext _db;

    public OrderGateway(AppDbContext db)
    {
        _db = db;
    }

    public List<Order> FindAll()
        => _db.Orders
              .OrderByDescending(o => o.Orderdate)
              .ToList();

    public Order? FindById(int orderId)
        => _db.Orders.FirstOrDefault(o => o.Orderid == orderId);
}