using Microsoft.AspNetCore.Mvc;
using ProRental.Interfaces.Module3.P2_5;

namespace ProRental.Controllers.Module3.P2_5;

public class CarbonFootprintController : Controller
{
    private readonly IPackagingProfilerControl _packagingControl;

    public CarbonFootprintController(IPackagingProfilerControl packagingControl)
    {
        _packagingControl = packagingControl;
    }

    public IActionResult DisplayAllPackagingFootprint()
    {
        var footprints = _packagingControl.GetAllPackagingFootprints();
        return View("~/Views/Module3/P2-5/PackagingFootprintView.cshtml", footprints);
    }
}
