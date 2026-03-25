using ProRental.Data.Module3.P2_1.Interfaces;
using ProRental.Data.UnitOfWork;
using ProRental.Domain.Entities;
using ProRental.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ProRental.Data.Module3.P2_1.Mappers;

public class DeliveryBatchMapper : IDeliveryBatchMapper
{
    private readonly AppDbContext _context;

    public DeliveryBatchMapper(AppDbContext context)
    {
        _context = context;
    }

    public DeliveryBatch CreateNewBatch(int destHubID, string destinationAddress)
    {
        var batch = DeliveryBatch.Create(0, destHubID, destinationAddress);
        _context.DeliveryBatches.Add(batch);
        _context.SaveChanges();
        return batch;
    }

    public DeliveryBatch? FindByIdentifier(string batchIdentifier)
    {
        if (!int.TryParse(batchIdentifier, out var batchId))
        {
            return null;
        }

        return _context.DeliveryBatches
            .Include(batch => batch.BatchOrders)
            .FirstOrDefault(batch => EF.Property<int>(batch, "DeliveryBatchId") == batchId);
    }

    public List<DeliveryBatch> FindAll()
    {
        return _context.DeliveryBatches
            .Include(batch => batch.BatchOrders)
            .OrderBy(batch => EF.Property<int>(batch, "DeliveryBatchId"))
            .ToList();
    }

    public List<DeliveryBatch> GetAllPendingBatches()
    {
        return FindAll()
            .Where(batch => string.Equals(batch.ReadDeliveryBatchStatus(), BatchStatus.PENDING.ToString(), StringComparison.Ordinal))
            .ToList();
    }

    public List<DeliveryBatch> GetBatchByStatus(string warehouse, BatchStatus status)
    {
        var normalizedStatus = status.ToString();
        return FindAll()
            .Where(batch =>
                string.Equals(batch.ReadDeliveryBatchStatus(), normalizedStatus, StringComparison.Ordinal)
                && string.Equals(batch.ReadSourceHub().ToString(), warehouse, StringComparison.Ordinal))
            .ToList();
    }

    public bool Insert(DeliveryBatch batch)
    {
        var exists = _context.DeliveryBatches
            .Any(existing => EF.Property<int>(existing, "DeliveryBatchId") == batch.ReadDeliveryBatchIdentifier());
        if (exists)
        {
            return false;
        }

        _context.DeliveryBatches.Add(batch);
        _context.SaveChanges();
        return true;
    }

    public bool Update(DeliveryBatch batch)
    {
        var exists = _context.DeliveryBatches
            .Any(existing => EF.Property<int>(existing, "DeliveryBatchId") == batch.ReadDeliveryBatchIdentifier());
        if (!exists)
        {
            return false;
        }

        _context.DeliveryBatches.Update(batch);
        _context.SaveChanges();
        return true;
    }

    public bool Delete(string batchIdentifier)
    {
        if (!int.TryParse(batchIdentifier, out var batchId))
        {
            return false;
        }

        var batch = _context.DeliveryBatches
            .Include(item => item.BatchOrders)
            .FirstOrDefault(item => EF.Property<int>(item, "DeliveryBatchId") == batchId);
        if (batch is null)
        {
            return false;
        }

        if (batch.BatchOrders.Count > 0)
        {
            _context.BatchOrders.RemoveRange(batch.BatchOrders);
        }

        _context.DeliveryBatches.Remove(batch);
        _context.SaveChanges();
        return true;
    }
}
