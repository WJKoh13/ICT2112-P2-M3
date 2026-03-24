using Microsoft.EntityFrameworkCore;
using ProRental.Data.UnitOfWork;
using ProRental.Domain.Entities;
using ProRental.Domain.Enums;
using ProRental.Interfaces.Module3.P2_1;

namespace ProRental.Domain.Controls;

/// <summary>
/// Main routing orchestrator for Module 3 / P2-1. It creates a structured
/// multi-modal route for Feature 1 and exposes first-mile route lookup for Feature 6.
/// </summary>
public sealed class RouteManager : IRoutingService, IRouteQueryService
{
    private readonly AppDbContext _context;
    private readonly IRouteLegBuilder _routeLegBuilder;

    public RouteManager(AppDbContext context, IRouteLegBuilder routeLegBuilder)
    {
        _context = context;
        _routeLegBuilder = routeLegBuilder;
    }

    public DeliveryRoute CreateMultiModalRoute(string origin, string destination, List<TransportMode> modes)
    {
        if (string.IsNullOrWhiteSpace(origin))
        {
            throw new ArgumentException("Origin is required.", nameof(origin));
        }

        if (string.IsNullOrWhiteSpace(destination))
        {
            throw new ArgumentException("Destination is required.", nameof(destination));
        }

        if (modes is null || modes.Count == 0)
        {
            throw new ArgumentException("At least one transport mode is required.", nameof(modes));
        }

        var route = new DeliveryRoute();
        route.SetOriginAddress(origin);
        route.SetDestinationAddress(destination);
        route.SetIsValid(true);

        var routeLegs = BuildRouteLegs(origin, destination, modes);
        foreach (var routeLeg in routeLegs)
        {
            route.RouteLegs.Add(routeLeg);
        }

        route.SetTotalDistanceKm(Math.Round(routeLegs.Sum(routeLeg => routeLeg.GetDistanceKm() ?? 0d), 2, MidpointRounding.AwayFromZero));

        _context.DeliveryRoutes.Add(route);
        _context.SaveChanges();

        return route;
    }

    public RouteLeg RetrieveFirstMileLeg(int routeId)
    {
        if (routeId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(routeId));
        }

        return _context.RouteLegs
            .AsNoTracking()
            .FirstOrDefault(routeLeg =>
                EF.Property<int>(routeLeg, "RouteId") == routeId &&
                (routeLeg.GetIsFirstMile() ?? false))
            ?? throw new InvalidOperationException($"First-mile route leg for route '{routeId}' was not found.");
    }

    private List<RouteLeg> BuildRouteLegs(string origin, string destination, IReadOnlyList<TransportMode> modes)
    {
        var routeLegs = new List<RouteLeg>();

        if (modes.Count == 1)
        {
            routeLegs.Add(_routeLegBuilder.BuildMainTransportLeg(1, origin, destination, modes[0]));
            return routeLegs;
        }

        routeLegs.Add(_routeLegBuilder.BuildFirstMileLeg(origin, modes[0]));

        for (var index = 1; index < modes.Count; index++)
        {
            var currentMode = modes[index];
            var previousMode = modes[index - 1];
            var isFinalMode = index == modes.Count - 1;

            if (isFinalMode)
            {
                routeLegs.Add(_routeLegBuilder.BuildLastMileLeg(routeLegs.Count + 1, previousMode, destination));
                break;
            }

            routeLegs.Add(_routeLegBuilder.BuildMainTransportLeg(
                routeLegs.Count + 1,
                $"{previousMode} Hub",
                $"{currentMode} Hub",
                currentMode));
        }

        return routeLegs;
    }
}
