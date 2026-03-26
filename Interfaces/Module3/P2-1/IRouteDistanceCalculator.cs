using ProRental.Domain.Enums;

namespace ProRental.Interfaces.Module3.P2_1;

public interface IRouteDistanceCalculator
{
    Task<double> CalculateDistanceKmAsync(
        TransportMode transportMode,
        RouteDistancePoint startPoint,
        RouteDistancePoint endPoint);
}
