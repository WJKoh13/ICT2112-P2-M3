using ProRental.Domain.Entities;

namespace ProRental.Interfaces.Module3.P2_1;

public interface IHubInfoService
{
    string FindNearestWarehouse(double latitude, double longitude);
    TransportationHub GetHubInfo(int hubId);
    List<TransportationHub> GetAllHubs();
}
