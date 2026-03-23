using ProRental.Data.Module3.P2_5.Interfaces;
using ProRental.Data.Module2.Interfaces;
using ProRental.Interfaces.Module3.P2_5;

namespace ProRental.Domain.Module3.P2_5.Controls;

public class PackagingFootprintControl : IPackagingFootprintControl
{
    // Carbon emission factors (kg CO2 per unit) by material type.
    // Source: industry averages; to be replaced.
    private static readonly Dictionary<string, float> CarbonFactors = new(StringComparer.OrdinalIgnoreCase)
    {
        { "Foam",      2.5f },
        { "Plastic",   1.8f },
        { "Cardboard", 0.4f },
        { "Paper",     0.3f },
        { "Fabric",    0.6f },
        { "Chemical",  3.2f },
    };
    private const float DefaultCarbonFactor = 1.0f;

    private readonly IOrderService _orderService;
    private readonly IPackagingProfileGateway _profileGateway;

    public PackagingFootprintControl(
        IOrderService orderService,
        IPackagingProfileGateway profileGateway)
    {
        _orderService   = orderService;
        _profileGateway = profileGateway;
    }

    public float CalculatePackagingFootprint(PackagingConfiguration configuration)
    {
        var primary = CalculateForMaterials(configuration.GetPrimaryMaterials(), weight: 1.0f);
        var secondary = CalculateForMaterials(configuration.GetSecondaryMaterials(), weight: 0.8f);
        return primary + secondary;
    }
    
    public List<dynamic> GetAllPackagingFootprints()
    {
        try
        {
            var orders = _orderService.GetAllOrders().DistinctBy(o => o.OrderId).ToDictionary(o => o.OrderId, o => o);

            var rows = _profileGateway.GetAllFootprintDetails();

            if (rows == null || !rows.Any())
                return new List<dynamic>();
            
            var results = rows.Select(row =>
            {
                var factor = CarbonFactors.TryGetValue((string)(row.MaterialType ?? ""), out var f) ? f : DefaultCarbonFactor;
                var category = (string)(row.Category ?? "secondary");
                var weight = category.Equals("primary", StringComparison.OrdinalIgnoreCase) ? 1.0f : 0.8f;
                var footprintKg = factor * (int)row.Quantity * weight;

                var productName = orders.TryGetValue((int)row.OrderId, out var o) ? o.ProductName : "Unknown";

                return (dynamic)new
                {
                    row.OrderId,
                    ProductName = productName,
                    row.ProfileId,
                    row.FragilityLevel,
                    row.Volume,
                    row.Category,
                    row.MaterialName,
                    row.MaterialType,
                    row.Quantity,
                    row.Recyclable,
                    row.Reusable,
                    FootprintKg = footprintKg
                };
            }).ToList();

            return results;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"[PackagingFootprintControl] Error retrieving footprint data: {ex.Message}");
            return new List<dynamic>();
        }
    }

    private static float CalculateForMaterials(Dictionary<PackagingMaterial, int> materials, float weight)
    {
        float total = 0f;
        foreach (var (material, quantity) in materials)
        {
            var factor = CarbonFactors.TryGetValue(material.GetMaterialType(), out var f) ? f : DefaultCarbonFactor;
            total += factor * quantity * weight;
        }
        return total;
    }
}
