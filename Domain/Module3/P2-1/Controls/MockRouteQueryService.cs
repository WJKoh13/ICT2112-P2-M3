using ProRental.Domain.Entities;
using ProRental.Interfaces.Module3.P2_1;

namespace ProRental.Domain.Module3.P2_1.Controls;

public class MockRouteQueryService : IRouteQueryService
{
    public RouteLeg RetrieveFirstMileLeg(int routeId)
    {
        return RouteLeg.CreateFirstMileLeg(routeId, "MAIN-HUB", "1", 12d);
    }
}
