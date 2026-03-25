using Microsoft.AspNetCore.Mvc;
using ProRental.Interfaces.Module3.P2_1;

namespace ProRental.Controllers.Module3.P2_1;

[Route("module3/p2-1/batch-delivery")]
public class BatchDeliveryController : Controller
{
    private readonly IBatchDisplayManager _batchDisplayManager;

    public BatchDeliveryController(IBatchDisplayManager batchDisplayManager)
    {
        _batchDisplayManager = batchDisplayManager;
    }

    [HttpGet("view")]
    public IActionResult DisplayBatchTable()
    {
        var model = _batchDisplayManager.GetBatchesForDisplay();

        return View("~/Views/Module3/P2-1/BatchDeliveryView.cshtml", model);
    }

    [HttpGet("refresh")]
    public IActionResult RefreshBatchView()
    {
        return RedirectToAction(nameof(DisplayBatchTable));
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        return RedirectToAction(nameof(DisplayBatchTable));
    }
}