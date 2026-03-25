namespace ProRental.Data.Module3.P2_1.Interfaces;

public interface IBatchOrderMapper
{
    bool AddOrderToBatch(int batchId, string orderId);
    bool RemoveOrderFromBatch(string batchId, string orderId);
    List<string> GetOrderIdsByBatch(int batchId);
    string? GetBatchIdByOrder(string orderId);
    bool OrderAlreadyAssigned(string orderId);
    int CountOrdersInBatch(int batchId);
}
