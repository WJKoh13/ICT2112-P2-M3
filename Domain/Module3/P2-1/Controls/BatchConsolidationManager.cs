using Microsoft.EntityFrameworkCore;
using ProRental.Data.Module3.P2_1.Interfaces;
using ProRental.Data.UnitOfWork;
using ProRental.Domain.Entities;
using ProRental.Domain.Enums;
using ProRental.Interfaces.Module3.P2_1;

namespace ProRental.Domain.Module3.P2_1.Controls;

public sealed class BatchConsolidationManager : IBatchDelivery
{
    private const double ConsolidationEfficiencyFactor = 0.75d;
    private const int ShowcaseFallbackRouteId = 2;

    private readonly IBatchValidator _batchValidator;
    private readonly IOrderService _orderService;
    private readonly IRouteQueryService _routeQueryService;
    private readonly ITransportCarbonService _transportCarbonService;
    private readonly IHubInfoService _hubInfoService;
    private readonly IDeliveryBatchMapper _deliveryBatchMapper;
    private readonly IBatchOrderMapper _batchOrderMapper;
    private readonly AppDbContext _context;

    public BatchConsolidationManager(
        IBatchValidator batchValidator,
        IOrderService orderService,
        IRouteQueryService routeQueryService,
        ITransportCarbonService transportCarbonService,
        IHubInfoService hubInfoService,
        IDeliveryBatchMapper deliveryBatchMapper,
        IBatchOrderMapper batchOrderMapper,
        AppDbContext context)
    {
        _batchValidator = batchValidator;
        _orderService = orderService;
        _routeQueryService = routeQueryService;
        _transportCarbonService = transportCarbonService;
        _hubInfoService = hubInfoService;
        _deliveryBatchMapper = deliveryBatchMapper;
        _batchOrderMapper = batchOrderMapper;
        _context = context;
    }

    public void consolidateOrderToBatch(string orderId)
    {
        if (!_batchValidator.validateOrderExists(orderId))
        {
            handleConsolidationFailure(orderId, "Order does not exist.");
        }

        if (_batchOrderMapper.orderAlreadyAssigned(orderId))
        {
            handleConsolidationFailure(orderId, "Order is already assigned to a batch.");
        }

        var shippingContext = _orderService.GetShippingContextAsync(int.Parse(orderId)).GetAwaiter().GetResult();
        if (shippingContext is null || string.IsNullOrWhiteSpace(shippingContext.DestinationAddress))
        {
            handleConsolidationFailure(orderId, "Order does not have a valid delivery address.");
        }

        if (!int.TryParse(orderId, out var parsedOrderId))
        {
            handleConsolidationFailure(orderId, "Order ID is invalid.");
        }

        var destinationHubId = ResolveBatchHubFromSequenceTwo(parsedOrderId).ToString();

        if (!batchOrderConsolidator(orderId, destinationHubId))
        {
            handleConsolidationFailure(orderId, "Unable to add order to pending batch.");
        }
    }

    public bool batchOrderConsolidator(string orderId, string destHub)
    {
        var pendingBatches = _deliveryBatchMapper.getBatchByStatus(destHub, BatchStatus.PENDING);
        var targetBatch = pendingBatches.FirstOrDefault();

        if (targetBatch is null)
        {
            if (!int.TryParse(destHub, out var destinationHubId))
            {
                handleConsolidationFailure(orderId, "Destination hub is invalid.");
            }

            var hub = _hubInfoService.GetHubInfo(destinationHubId);
            if (hub is null)
            {
                throw new InvalidOperationException($"Consolidation failed for order '{orderId}': destination hub could not be resolved.");
            }

            targetBatch = createNewBatch(destinationHubId, hub.GetAddress());
        }

        var wasAdded = _batchOrderMapper.addOrderToBatch(targetBatch.GetDeliveryBatchIdentifier(), orderId);
        if (!wasAdded)
        {
            return false;
        }

        RecalculateBatchMetrics(targetBatch.GetDeliveryBatchIdentifier());
        return true;
    }

    public void handleConsolidationFailure(string orderId, string reason)
    {
        throw new InvalidOperationException($"Consolidation failed for order '{orderId}': {reason}");
    }

    public double batchCarbonCostSavings(double unconsolidatedCost, List<string> consOrderIds)
    {
        if (consOrderIds.Count <= 1)
        {
            return 0d;
        }

        var distanceKm = _routeQueryService.retrieveFirstMileLeg(1)?.GetDistanceKm() ?? 0d;
        var batchWeightKg = CalculateBatchWeight(consOrderIds);

        var consolidatedLegCost = _transportCarbonService.CalculateLegCarbon(
            quantity: 1,
            weightKg: batchWeightKg,
            distanceKm: distanceKm,
            storageCo2: 0d);

        var adjustedConsolidatedCost = consolidatedLegCost * ConsolidationEfficiencyFactor;
        return Math.Max(0d, unconsolidatedCost - adjustedConsolidatedCost);
    }

    public DeliveryBatch createNewBatch(int destHubID, string destinationAddress)
    {
        var newBatch = _deliveryBatchMapper.createNewBatch(destHubID, destinationAddress);
        _deliveryBatchMapper.insert(newBatch);
        return newBatch;
    }

    public bool markBatchesAsShipped(List<string> batchIds)
    {
        foreach (var batchId in batchIds)
        {
            if (!int.TryParse(batchId, out var parsedBatchId))
            {
                return false;
            }

            if (!_batchValidator.validateBatchExists(parsedBatchId))
            {
                return false;
            }

            var batch = _deliveryBatchMapper.findByIdentifier(batchId);
            if (batch is null)
            {
                return false;
            }

            batch.markAsShipped();
            _deliveryBatchMapper.update(batch);
        }

        return true;
    }

    public bool resetOrderBatchAssignments()
    {
        var links = _context.BatchOrders.ToList();
        if (links.Count > 0)
        {
            _context.BatchOrders.RemoveRange(links);
        }

        var batches = _deliveryBatchMapper.findAll();
        foreach (var batch in batches)
        {
            batch.SetTotalOrders(0);
            batch.updateBatchWeight(0d);
            batch.updateCarbonSavings(0d);
            _context.DeliveryBatches.Update(batch);
        }

        return _context.SaveChanges() >= 0;
    }

    public double CalculateUnconsolidatedFirstLegCost(IEnumerable<string> orderIds, double distanceKm)
    {
        var total = 0d;

        foreach (var orderId in orderIds)
        {
            if (!int.TryParse(orderId, out var parsedOrderId))
            {
                continue;
            }

            var orderWeightKg = GetOrderWeightKg(parsedOrderId);
            total += _transportCarbonService.CalculateLegCarbon(
                quantity: 1,
                weightKg: orderWeightKg,
                distanceKm: distanceKm,
                storageCo2: 0d);
        }

        return total;
    }

    public double CalculateBatchWeight(IEnumerable<string> orderIds)
    {
        var totalWeight = 0d;

        foreach (var orderId in orderIds)
        {
            if (!int.TryParse(orderId, out var parsedOrderId))
            {
                continue;
            }

            totalWeight += GetOrderWeightKg(parsedOrderId);
        }

        return Math.Max(totalWeight, 1d);
    }

    public void RecalculateBatchMetrics(int batchId)
    {
        var batch = _deliveryBatchMapper.findByIdentifier(batchId.ToString());
        if (batch is null)
        {
            return;
        }

        var orderIds = _batchOrderMapper.getOrderIdsByBatch(batchId);
        var totalOrders = _batchOrderMapper.countOrdersInBatch(batchId);
        var distanceKm = _routeQueryService.retrieveFirstMileLeg(1)?.GetDistanceKm() ?? 0d;

        var batchWeightKg = CalculateBatchWeight(orderIds);
        var unconsolidatedCost = CalculateUnconsolidatedFirstLegCost(orderIds, distanceKm);
        var carbonSavings = batchCarbonCostSavings(unconsolidatedCost, orderIds);

        batch.SetTotalOrders(totalOrders);
        batch.updateBatchWeight(batchWeightKg);
        batch.updateCarbonSavings(carbonSavings);
        _deliveryBatchMapper.update(batch);
    }

    public double GetOrderWeightKg(int orderId)
    {
        var orderWeightKg = (from orderItem in _context.Orderitems
                             join productDetail in _context.Productdetails
                                 on EF.Property<int>(orderItem, "Productid") equals EF.Property<int>(productDetail, "Productid")
                             where EF.Property<int>(orderItem, "Orderid") == orderId
                             select (double?)EF.Property<int>(orderItem, "Quantity") *
                                    (double?)(EF.Property<decimal?>(productDetail, "Weight") ?? 1m))
            .Sum() ?? 0d;

        return orderWeightKg > 0d ? orderWeightKg : 1d;
    }

    private int ResolveBatchHubFromSequenceTwo(int orderId)
    {
        var selectedRouteId = _context.ShippingOptions
            .Where(shippingOption => EF.Property<int?>(shippingOption, "OrderId") == orderId)
            .OrderByDescending(shippingOption => EF.Property<int>(shippingOption, "OptionId"))
            .Select(shippingOption => EF.Property<int?>(shippingOption, "RouteId"))
            .FirstOrDefault();

        var routeIdToUse = selectedRouteId.HasValue && selectedRouteId.Value > 0
            ? selectedRouteId.Value
            : ShowcaseFallbackRouteId;

        var sequenceTwoStartPoint = _context.RouteLegs
            .Where(routeLeg =>
                EF.Property<int>(routeLeg, "RouteId") == routeIdToUse
                && EF.Property<int>(routeLeg, "Sequence") == 2)
            .Select(routeLeg => EF.Property<string>(routeLeg, "StartPoint"))
            .FirstOrDefault();

        if (string.IsNullOrWhiteSpace(sequenceTwoStartPoint))
        {
            throw new InvalidOperationException($"Consolidation failed for order '{orderId}': route leg sequence 2 was not found for route '{routeIdToUse}'.");
        }

        var hubId = _context.TransportationHubs
            .Where(hub => EF.Property<string>(hub, "Address") == sequenceTwoStartPoint)
            .Select(hub => EF.Property<int>(hub, "HubId"))
            .FirstOrDefault();

        if (hubId <= 0)
        {
            throw new InvalidOperationException($"Consolidation failed for order '{orderId}': no transportation hub matched sequence-2 start point '{sequenceTwoStartPoint}'.");
        }

        return hubId;
    }
}
