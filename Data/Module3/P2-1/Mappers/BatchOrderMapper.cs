using ProRental.Data.Module3.P2_1.Interfaces;
using ProRental.Data.UnitOfWork;
using ProRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProRental.Data.Module3.P2_1.Mappers;

public class BatchOrderMapper : IBatchOrderMapper
{
    private readonly AppDbContext _context;

    public BatchOrderMapper(AppDbContext context)
    {
        _context = context;
    }

    public bool AddOrderToBatch(int batchId, string orderId)
    {
        if (!int.TryParse(orderId, out var parsedOrderId))
        {
            return false;
        }

        var batchExists = _context.DeliveryBatches.Any(batch => EF.Property<int>(batch, "DeliveryBatchId") == batchId);
        if (!batchExists)
        {
            return false;
        }

        var existingLink = _context.BatchOrders
            .Any(link => EF.Property<int>(link, "OrderId") == parsedOrderId);
        if (existingLink)
        {
            return false;
        }

        var batchOrder = BatchOrder.Create(batchId, parsedOrderId);
        _context.BatchOrders.Add(batchOrder);
        _context.SaveChanges();
        return true;
    }

    public bool RemoveOrderFromBatch(string batchId, string orderId)
    {
        if (!int.TryParse(batchId, out var parsedBatchId) || !int.TryParse(orderId, out var parsedOrderId))
        {
            return false;
        }

        var link = _context.BatchOrders
            .FirstOrDefault(item => EF.Property<int>(item, "BatchId") == parsedBatchId
                && EF.Property<int>(item, "OrderId") == parsedOrderId);

        if (link is null)
        {
            return false;
        }

        _context.BatchOrders.Remove(link);
        _context.SaveChanges();
        return true;
    }

    public List<string> GetOrderIdsByBatch(int batchId)
    {
        return _context.BatchOrders
            .Where(link => EF.Property<int>(link, "BatchId") == batchId)
            .Select(link => EF.Property<int>(link, "OrderId").ToString())
            .ToList();
    }

    public string? GetBatchIdByOrder(string orderId)
    {
        if (!int.TryParse(orderId, out var parsedOrderId))
        {
            return null;
        }

        var link = _context.BatchOrders
            .FirstOrDefault(item => EF.Property<int>(item, "OrderId") == parsedOrderId);

        if (link is null)
        {
            return null;
        }

        return link.ReadBatchId().ToString();
    }

    public bool OrderAlreadyAssigned(string orderId)
    {
        return !string.IsNullOrWhiteSpace(GetBatchIdByOrder(orderId));
    }

    public int CountOrdersInBatch(int batchId)
    {
        return _context.BatchOrders.Count(link => EF.Property<int>(link, "BatchId") == batchId);
    }
}