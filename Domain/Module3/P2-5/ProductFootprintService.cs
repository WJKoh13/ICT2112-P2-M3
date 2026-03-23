// Domain/Module3/P2-5/ProductFootprintServiceStub.cs
// Temporary stub for IProductFootprintService.
// Allows Feature 4 to compile and run independently of Feature 1.
// DELETE this file and update the DI registration once Feature 1 is merged.

using ProRental.Interfaces.Domain;
using ProRental.Data.UnitOfWork;

namespace ProRental.Domain.Controls;

internal sealed class ProductFootprintService : IProductFootprintService
{
    private readonly AppDbContext _db;

    public ProductFootprintService(AppDbContext db)
    {
        _db = db;
    }

    public double CalculateProductFootprint(int orderId)
    {
        var orderItems = _db.Orderitems
            .Where(oi => oi.Orderid == orderId)
            .ToList();

        double total = 0;

        foreach (var item in orderItems)
        {
            var footprint = _db.Productfootprints
                .FirstOrDefault(p => p.Productid == item.Productid);

            if (footprint != null)
            {
                total += footprint.Totalco2 * item.Quantity;
            }
        }

        return total;
    }
}