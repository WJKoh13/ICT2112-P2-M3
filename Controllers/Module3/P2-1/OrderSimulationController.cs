using Microsoft.AspNetCore.Mvc;
using ProRental.Domain.Module3.P2_1.Models;
using ProRental.Interfaces.Module3.P2_1;

namespace ProRental.Controllers.Module3.P2_1;

[Route("module3/p2-1/order-simulation")]
public class OrderSimulationController : Controller
{
    private readonly IBatchDisplayManager _batchDisplayManager;
    private readonly IBatchDelivery _batchDelivery;
    private readonly IOrderSimulationService _orderSimulationService;

    public OrderSimulationController(
        IBatchDisplayManager batchDisplayManager,
        IBatchDelivery batchDelivery,
        IOrderSimulationService orderSimulationService)
    {
        _batchDisplayManager = batchDisplayManager;
        _batchDelivery = batchDelivery;
        _orderSimulationService = orderSimulationService;
    }

    [HttpGet("")]
    [HttpGet("view")]
    public IActionResult DisplaySimulation()
    {
        var orders = _orderSimulationService.GetAllOrders();
        var pendingBatches = _batchDisplayManager
            .GetBatchesForDisplay()
            .Where(batch => string.Equals(batch.ReadDeliveryBatchStatus(), "PENDING", StringComparison.Ordinal))
            .ToList();

        ViewBag.PendingBatches = pendingBatches;
        return View("~/Views/Module3/P2-1/BatchSimulationView.cshtml", orders);
    }

    [HttpPost("create-order")]
    [ValidateAntiForgeryToken]
    public IActionResult CreateOrder(CreateOrderRequest request)
    {
        try
        {
            var order = _orderSimulationService.CreateOrder(request);
            if (request.UseBatchShipping)
            {
                _batchDelivery.ConsolidateOrderToBatch(order.OrderId.ToString());
            }

            TempData["BatchSimulationMessage"] = $"Order {order.OrderId} created successfully.";
        }
        catch (Exception ex)
        {
            TempData["BatchSimulationError"] = ex.Message;
        }

        return RedirectToAction(nameof(DisplaySimulation));
    }

    [HttpPost("mark-shipped")]
    [ValidateAntiForgeryToken]
    public IActionResult MarkShipped(List<string> batchIds)
    {
        if (batchIds.Count == 0)
        {
            TempData["BatchSimulationError"] = "Select at least one batch to mark as shipped.";
            return RedirectToAction(nameof(DisplaySimulation));
        }

        var success = _batchDelivery.MarkBatchesAsShipped(batchIds);
        TempData["BatchSimulationMessage"] = success
            ? "Selected batches marked as shipped."
            : "Some batches could not be updated.";

        return RedirectToAction(nameof(DisplaySimulation));
    }

}