using ProRental.Domain.Entities;
using ProRental.Domain.Enums;

namespace ProRental.Data.Module3.P2_1.Interfaces;

public interface IDeliveryBatchMapper
{
    DeliveryBatch CreateNewBatch(int destHubID, string destinationAddress);
    DeliveryBatch? FindByIdentifier(string batchIdentifier);
    List<DeliveryBatch> FindAll();
    List<DeliveryBatch> GetAllPendingBatches();
    List<DeliveryBatch> GetBatchByStatus(string warehouse, BatchStatus status);
    bool Insert(DeliveryBatch batch);
    bool Update(DeliveryBatch batch);
    bool Delete(string batchIdentifier);
}