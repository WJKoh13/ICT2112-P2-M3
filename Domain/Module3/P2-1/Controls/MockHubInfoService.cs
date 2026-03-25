using ProRental.Domain.Entities;
using ProRental.Domain.Enums;
using ProRental.Interfaces.Module3.P2_1;

namespace ProRental.Domain.Module3.P2_1.Controls;

public class MockHubInfoService : IHubInfoService
{
    private static readonly List<TransportationHub> Hubs =
    [
        TransportationHub.Create(1, "Jurong Warehouse, Singapore", HubType.WAREHOUSE, 1.3388, 103.7064),
        TransportationHub.Create(2, "Tuas Warehouse, Singapore", HubType.WAREHOUSE, 1.3210, 103.6488),
        TransportationHub.Create(3, "Harbour Front Port, Singapore", HubType.SHIPPING_PORT, 1.2644, 103.8223)
    ];

    public string FindNearestWarehouse(double latitude, double longitude)
    {
        var nearest = Hubs
            .Where(hub => hub.ReadHubType() == HubType.WAREHOUSE)
            .OrderBy(hub => CalculateEuclideanDistance(latitude, longitude, hub.ReadLatitude(), hub.ReadLongitude()))
            .First();

        return nearest.ReadHubId().ToString();
    }

    public TransportationHub GetHubInfo(int hubId)
    {
        return Hubs.First(hub => hub.ReadHubId() == hubId);
    }

    public List<TransportationHub> GetAllHubs()
    {
        return Hubs.ToList();
    }

    private static double CalculateEuclideanDistance(double latA, double longA, double latB, double longB)
    {
        var latDiff = latA - latB;
        var longDiff = longA - longB;
        return Math.Sqrt((latDiff * latDiff) + (longDiff * longDiff));
    }
}
