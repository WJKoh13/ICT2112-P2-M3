using Microsoft.AspNetCore.Mvc;

namespace ProRental.Controllers.Module3.P2_5;

public class CarbonMonitoringController : Controller
{
    public IActionResult CarbonDashboardView()
    {
        return View("~/Views/Module3/P2-5/CarbonDashboardView.cshtml");
    }
}
