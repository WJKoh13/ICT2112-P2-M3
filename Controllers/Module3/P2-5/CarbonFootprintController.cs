using Microsoft.AspNetCore.Mvc;
using ProRental.Interfaces.Module3.P2_5;

namespace ProRental.Controllers.Module3.P2_5;

public class CarbonFootprintController : Controller
{
    private readonly IPackagingFootprintControl _packagingFootprintControl;

    public CarbonFootprintController(IPackagingFootprintControl packagingFootprintControl)
    {
        _packagingFootprintControl = packagingFootprintControl;
    }

    public IActionResult DisplayAllProductFootprint()
    {
        return View("~/Views/Module3/P2-5/ProductFootprintView.cshtml");
    }

    public IActionResult DisplayAllStaffFootprint()
    {
        return View("~/Views/Module3/P2-5/StaffFootprintView.cshtml");
    }

    public IActionResult DisplayAllBuildingFootprint()
    {
        return View("~/Views/Module3/P2-5/BuildingFootprintView.cshtml");
    }

    public IActionResult DisplayAllPackagingFootprint()
    {
        var results = _packagingFootprintControl.GetAllPackagingFootprints();
        return View("~/Views/Module3/P2-5/PackagingFootprintView.cshtml", results);
    }
}
