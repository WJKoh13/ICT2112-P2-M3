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

    [HttpPost]
    public IActionResult CreatePackagingProfile([FromBody] CreateProfileRequest req)
    {
        try
        {
            var profile = _packagingControl.CreatePackagingProfile(req.OrderId, req.Volume, req.FragilityLevel);
            if (profile.Packagingconfigurations != null && profile.Packagingconfigurations.Any())
            {
                return BadRequest(new { message = $"A packaging configuration already exists." });
            }

            _packagingControl.CreatePackagingConfiguration(profile);
            
            return Json(new { success = true, message = "Packaging profile and configuration successfully generated!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}

public class CreateProfileRequest
{
    public int OrderId { get; set; }
    public float Volume { get; set; }
    public string FragilityLevel { get; set; } = "low";
}
