using ProRental.Data.Module3.P2_1.Interfaces;
using ProRental.Domain.Entities;
using ProRental.Domain.Enums;
using ProRental.Interfaces.Module3.P2_1;

namespace ProRental.Domain.Module3.P2_1.Controls;

public class BatchConsolidationManager : IBatchDelivery
{
    private readonly IBatchValidator _batchValidator;
    private readonly IOrderService _orderService;
    private readonly IRouteQueryService _routeQueryService;
    private readonly ITransportCarbonService _transportCarbonService;
    private readonly IHubInfoService _hubInfoService;
    private readonly IDeliveryBatchMapper _deliveryBatchMapper;
    private readonly IBatchOrderMapper _batchOrderMapper;

    public BatchConsolidationManager(
        IBatchValidator batchValidator,
        IOrderService orderService,
        IRouteQueryService routeQueryService,
        ITransportCarbonService transportCarbonService,
        IHubInfoService hubInfoService,
        IDeliveryBatchMapper deliveryBatchMapper,
        IBatchOrderMapper batchOrderMapper)
    {
        _batchValidator = batchValidator;
        _orderService = orderService;
        _routeQueryService = routeQueryService;
        _transportCarbonService = transportCarbonService;
        _hubInfoService = hubInfoService;
        _deliveryBatchMapper = deliveryBatchMapper;
        _batchOrderMapper = batchOrderMapper;
    }

    public void ConsolidateOrderToBatch(string orderId)
    {
        if (!_batchValidator.ValidateOrderExists(orderId))
        {
            HandleConsolidationFailure(orderId, "Order does not exist.");
            return;
        }

        if (_batchOrderMapper.OrderAlreadyAssigned(orderId))
        {
            HandleConsolidationFailure(orderId, "Order is already assigned to a batch.");
            return;
        }

        if (!int.TryParse(orderId, out var parsedOrderId))
        {
            HandleConsolidationFailure(orderId, "Order ID format is invalid.");
            return;
        }

        var orderDetails = _orderService.OrderDetails(parsedOrderId);
        if (orderDetails.Count == 0)
        {
            HandleConsolidationFailure(orderId, "Order has no line items.");
            return;
        }

        if (orderDetails.Any(detail => detail.IsDispatched))
        {
            HandleConsolidationFailure(orderId, "Order has already been dispatched.");
            return;
        }

        if (orderDetails.All(detail => !detail.UsesBatchShipping))
        {
            HandleConsolidationFailure(orderId, "Order did not opt in for batch shipping.");
            return;
        }

        var routeLeg = _routeQueryService.RetrieveFirstMileLeg(1);
        var destinationHub = routeLeg.ReadEndPoint();
        BatchOrderConsolidator(orderId, destinationHub);
    }

    public bool BatchOrderConsolidator(string orderId, string destHub)
    {
        var pendingBatches = _deliveryBatchMapper.GetBatchByStatus(destHub, BatchStatus.PENDING);

        if (!int.TryParse(destHub, out var destinationHubId))
        {
            HandleConsolidationFailure(orderId, "Destination hub is invalid.");
            return false;
        }

        var warehouse = _hubInfoService.GetHubInfo(destinationHubId);
        if (warehouse.ReadHubType() != HubType.WAREHOUSE)
        {
            HandleConsolidationFailure(orderId, "Destination hub is not a warehouse.");
            return false;
        }

        var selectedBatch = pendingBatches.FirstOrDefault()
            ?? CreateNewBatch(destinationHubId, warehouse.ReadAddress());

        if (!_batchOrderMapper.AddOrderToBatch(selectedBatch.ReadDeliveryBatchIdentifier(), orderId))
        {
            HandleConsolidationFailure(orderId, "Unable to assign order to batch.");
            return false;
        }

        if (!int.TryParse(orderId, out var parsedOrderId))
        {
            HandleConsolidationFailure(orderId, "Order ID format is invalid.");
            return false;
        }

        selectedBatch.AddOrder(parsedOrderId);
        var orderIds = _batchOrderMapper.GetOrderIdsByBatch(selectedBatch.ReadDeliveryBatchIdentifier());
        var routeLeg = _routeQueryService.RetrieveFirstMileLeg(1);
        var unconsolidatedCost = CalculateUnconsolidatedFirstLegCost(orderIds, routeLeg.ReadDistanceKm());
        var totalWeight = CalculateBatchWeight(orderIds);
        selectedBatch.UpdateBatchWeight(totalWeight);
        selectedBatch.UpdateCarbonSavings(BatchCarbonCostSavings(unconsolidatedCost, orderIds));
        _deliveryBatchMapper.Update(selectedBatch);

        return true;
    }

    public void HandleConsolidationFailure(string orderId, string reason)
    {
        throw new InvalidOperationException($"Consolidation failed for order {orderId}: {reason}");
    }

    public double BatchCarbonCostSavings(double unconsolidatedCost, List<string> consOrderIds)
    {
        if (consOrderIds.Count < 2)
        {
            return 0;
        }

        var routeLeg = _routeQueryService.RetrieveFirstMileLeg(1);
        var distanceKm = routeLeg.ReadDistanceKm();
        var totalQuantity = 0;
        var totalWeight = 0d;

        foreach (var orderId in consOrderIds)
        {
            if (!int.TryParse(orderId, out var parsedOrderId))
            {
                continue;
            }

            var details = _orderService.OrderDetails(parsedOrderId);
            totalQuantity += details.Sum(detail => detail.Quantity);
            totalWeight += details.Sum(detail => detail.WeightKg);
        }

        var consolidatedCost = _transportCarbonService.CalculateLegCarbon(totalQuantity, totalWeight, distanceKm, 0);
        return Math.Max(0, unconsolidatedCost - consolidatedCost);
    }

    public DeliveryBatch CreateNewBatch(int destHubID, string destinationAddress)
    {
        return _deliveryBatchMapper.CreateNewBatch(destHubID, destinationAddress);
    }

    public bool MarkBatchesAsShipped(List<string> batchIds)
    {
        var success = true;
        foreach (var batchId in batchIds)
        {
            if (!int.TryParse(batchId, out var parsedBatchId) || !_batchValidator.ValidateBatchExists(parsedBatchId))
            {
                success = false;
                continue;
            }

            var batch = _deliveryBatchMapper.FindByIdentifier(batchId);
            if (batch is null)
            {
                success = false;
                continue;
            }

            batch.MarkAsShipped();
            _deliveryBatchMapper.Update(batch);
        }

        return success;
    }

    private double CalculateUnconsolidatedFirstLegCost(IEnumerable<string> orderIds, double distanceKm)
    {
        var total = 0d;
        foreach (var orderId in orderIds)
        {
            if (!int.TryParse(orderId, out var parsedOrderId))
            {
                continue;
            }

            var details = _orderService.OrderDetails(parsedOrderId);
            var quantity = details.Sum(detail => detail.Quantity);
            var weight = details.Sum(detail => detail.WeightKg);
            total += _transportCarbonService.CalculateLegCarbon(quantity, weight, distanceKm, 0);
        }

        return total;
    }

    private double CalculateBatchWeight(IEnumerable<string> orderIds)
    {
        var totalWeight = 0d;

        foreach (var orderId in orderIds)
        {
            if (!int.TryParse(orderId, out var parsedOrderId))
            {
                continue;
            }

            totalWeight += _orderService
                .OrderDetails(parsedOrderId)
                .Sum(detail => detail.WeightKg);
        }

        return totalWeight;
    }
}