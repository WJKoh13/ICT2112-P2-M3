using Microsoft.EntityFrameworkCore;
using ProRental.Data.UnitOfWork;
using ProRental.Data.Module2.Interfaces;

namespace ProRental.Data.Module2.Gateways;

public class OrderService : IOrderService
{
    private readonly AppDbContext _db;

    public OrderService(AppDbContext db)
    {
        _db = db;
    }

    // Joins Order → Orderitem → Product → Productdetail to retrieve
    // the order ID, product name, and weight needed for packaging profile creation.
    public List<OrderProductInfo> GetAllOrders()
    {
        return _db.Orders
            .Join(_db.Orderitems,
                order => EF.Property<int>(order, "Orderid"),
                item  => EF.Property<int>(item,  "Orderid"),
                (order, item) => new { order, item })
            .Join(_db.Products,
                oi      => EF.Property<int>(oi.item, "Productid"),
                product => EF.Property<int>(product, "Productid"),
                (oi, product) => new { oi.order, oi.item, product })
            .Join(_db.Productdetails,
                op     => EF.Property<int>(op.product, "Productid"),
                detail => EF.Property<int>(detail,     "Productid"),
                (op, detail) => new OrderProductInfo
                {
                    OrderId     = EF.Property<int>(op.order, "Orderid"),
                    ProductName = EF.Property<string>(detail, "Name"),
                    Weight      = (float)(EF.Property<decimal?>(detail, "Weight") ?? 1.0m)
                })
            .ToList();
    }
}
