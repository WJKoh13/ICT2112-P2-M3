using ProRental.Data.Module3.P2_1.Interfaces;
using ProRental.Interfaces.Module3.P2_1;

namespace ProRental.Domain.Module3.P2_1.Controls;

public class BatchValidator : IBatchValidator
{
    private readonly IDeliveryBatchMapper _deliveryBatchMapper;
    private readonly IOrderService _orderService;

    public BatchValidator(IDeliveryBatchMapper deliveryBatchMapper, IOrderService orderService)
    {
        _deliveryBatchMapper = deliveryBatchMapper;
        _orderService = orderService;
    }

    public bool ValidateOrderExists(string orderId)
    {
        if (!int.TryParse(orderId, out var parsedOrderId))
        {
            return false;
        }

        return _orderService.OrderDetails(parsedOrderId).Count > 0;
    }

    public bool ValidateBatchExists(int batchId)
    {
        return _deliveryBatchMapper.FindByIdentifier(batchId.ToString()) is not null;
    }
}
