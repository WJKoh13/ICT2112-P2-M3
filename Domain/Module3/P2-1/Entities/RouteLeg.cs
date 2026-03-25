using ProRental.Domain.Enums;

namespace ProRental.Domain.Entities;

public partial class RouteLeg
{
    private TransportMode? _transportMode;
    private TransportMode? TransportMode { get => _transportMode; set => _transportMode = value; }
    public void UpdateTransportMode(TransportMode newValue) => _transportMode = newValue;

    public static RouteLeg CreateFirstMileLeg(int routeId, string startPoint, string endPoint, double distanceKm)
    {
        var leg = new RouteLeg();
        leg._routeId = routeId;
        leg._startPoint = startPoint;
        leg._endPoint = endPoint;
        leg._distanceKm = distanceKm;
        leg._isFirstMile = true;
        leg._isLastMile = false;
        leg._transportMode = global::ProRental.Domain.Enums.TransportMode.TRUCK;
        return leg;
    }

    public int ReadRouteId() => _routeId;
    public string ReadStartPoint() => _startPoint ?? string.Empty;
    public string ReadEndPoint() => _endPoint ?? string.Empty;
    public double ReadDistanceKm() => _distanceKm ?? 0d;
    public bool ReadIsFirstMile() => _isFirstMile ?? false;
    public global::ProRental.Domain.Enums.TransportMode ReadTransportMode() => _transportMode ?? global::ProRental.Domain.Enums.TransportMode.TRUCK;
}
