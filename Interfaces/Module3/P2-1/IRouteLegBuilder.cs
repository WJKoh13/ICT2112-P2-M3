using ProRental.Domain.Entities;
using ProRental.Domain.Enums;

namespace ProRental.Interfaces.Module3.P2_1;

public interface IRouteLegBuilder
{
    Task<RouteLeg> BuildFirstMileLegAsync(RouteDistancePoint startPoint, RouteDistancePoint endPoint);
    Task<RouteLeg> BuildMainTransportLegAsync(int sequence, RouteDistancePoint startPoint, RouteDistancePoint endPoint, TransportMode transportMode);
    Task<RouteLeg> BuildLastMileLegAsync(int sequence, RouteDistancePoint startPoint, RouteDistancePoint endPoint);
}
