using ProRental.Interfaces.Module3.P2_1;

namespace ProRental.Domain.Module3.P2_1.Controls;

public sealed class RouteDistanceCalculator : IRouteDistanceCalculator
{
    private static readonly Dictionary<(string StartPoint, string EndPoint), double> DistancesByLeg = new()
    {
        { ("ProRental Warehouse", "PLANE Hub"), 18d },
        { ("ProRental Warehouse", "SHIP Hub"), 42d },
        { ("ProRental Warehouse", "TRAIN Hub"), 26d },
        { ("ProRental Warehouse", "TRUCK Hub"), 8d },
        { ("PLANE Hub", "TRUCK Hub"), 12d },
        { ("SHIP Hub", "TRUCK Hub"), 15d },
        { ("TRAIN Hub", "TRUCK Hub"), 10d }
    };

    public double CalculateDistanceKm(string startPoint, string endPoint)
    {
        if (!DistancesByLeg.TryGetValue((startPoint, endPoint), out var distanceKm))
        {
            distanceKm = CalculateFallbackDistanceKm(startPoint, endPoint);
        }

        return distanceKm;
    }

    private static double CalculateFallbackDistanceKm(string startPoint, string endPoint)
    {
        // Keep a deterministic local fallback so the routing seam stays demo-safe
        // until a real external distance provider is introduced.
        var combinedLength = Math.Max(startPoint.Length + endPoint.Length, 1);
        return Math.Round((combinedLength % 23) + 7d, 2, MidpointRounding.AwayFromZero);
    }
}
