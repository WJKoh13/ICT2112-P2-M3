using ProRental.Data.Module3.P2_1.Interfaces;
using ProRental.Domain.Entities;
using ProRental.Interfaces.Module3.P2_1;

namespace ProRental.Domain.Module3.P2_1.Controls;

public class BatchQueryManager : IBatchDisplayManager, IBatchQueryManager
{
    private readonly IDeliveryBatchMapper _deliveryBatchMapper;

    public BatchQueryManager(IDeliveryBatchMapper deliveryBatchMapper)
    {
        _deliveryBatchMapper = deliveryBatchMapper;
    }

    public List<string> GetBatches()
    {
        return _deliveryBatchMapper
            .FindAll()
            .Select(batch => batch.ReadDeliveryBatchIdentifier().ToString())
            .ToList();
    }

    public List<DeliveryBatch> GetBatchesForDisplay()
    {
        return _deliveryBatchMapper
            .FindAll()
            .OrderBy(batch => batch.ReadDeliveryBatchIdentifier())
            .ToList();
    }
}
