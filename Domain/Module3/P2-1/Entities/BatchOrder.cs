namespace ProRental.Domain.Entities;

public partial class BatchOrder
{
    public static BatchOrder Create(int batchId, int orderId)
    {
        var batchOrder = new BatchOrder();
        batchOrder._batchId = batchId;
        batchOrder._orderId = orderId;
        batchOrder._addedTimestamp = DateTime.UtcNow;
        return batchOrder;
    }

    public int ReadBatchId() => _batchId;
    public int ReadOrderId() => _orderId;
    public DateTime? ReadAddedTimestamp() => _addedTimestamp;
}