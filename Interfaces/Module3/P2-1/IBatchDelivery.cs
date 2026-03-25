namespace ProRental.Interfaces.Module3.P2_1;

public interface IBatchDelivery
{
    void ConsolidateOrderToBatch(string orderId);
    bool MarkBatchesAsShipped(List<string> batchIds);
}