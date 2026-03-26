using ProRental.Domain.Entities;
using ProRental.Domain.Enums;
using ProRental.Interfaces.Module3.P2_1;

namespace ProRental.Domain.Controls;

/// <summary>
/// Builds route legs for the routing workflow while keeping leg construction
/// separate from route orchestration.
/// </summary>
public sealed class RouteLegBuilder : IRouteLegBuilder
{
    private readonly IRouteDistanceCalculator _routeDistanceCalculator;

    public RouteLegBuilder(IRouteDistanceCalculator routeDistanceCalculator)
    {
        _routeDistanceCalculator = routeDistanceCalculator;
    }

    public Task<RouteLeg> BuildFirstMileLegAsync(RouteDistancePoint startPoint, RouteDistancePoint endPoint)
    {
        return BuildLegAsync(1, startPoint, endPoint, TransportMode.TRUCK, isFirstMile: true, isMainTransport: false, isLastMile: false);
    }

    public Task<RouteLeg> BuildMainTransportLegAsync(int sequence, RouteDistancePoint startPoint, RouteDistancePoint endPoint, TransportMode transportMode)
    {
        return BuildLegAsync(sequence, startPoint, endPoint, transportMode, isFirstMile: false, isMainTransport: true, isLastMile: false);
    }

    public Task<RouteLeg> BuildLastMileLegAsync(int sequence, RouteDistancePoint startPoint, RouteDistancePoint endPoint)
    {
        return BuildLegAsync(sequence, startPoint, endPoint, TransportMode.TRUCK, isFirstMile: false, isMainTransport: false, isLastMile: true);
    }

    private async Task<RouteLeg> BuildLegAsync(
        int sequence,
        RouteDistancePoint startPoint,
        RouteDistancePoint endPoint,
        TransportMode transportMode,
        bool isFirstMile,
        bool isMainTransport,
        bool isLastMile)
    {
        var distanceKm = await _routeDistanceCalculator.CalculateDistanceKmAsync(transportMode, startPoint, endPoint);
        var routeLeg = new RouteLeg();
        routeLeg.ConfigureLeg(sequence, startPoint.Address, endPoint.Address, distanceKm, transportMode, isFirstMile, isMainTransport, isLastMile);
        return routeLeg;
    }
}
