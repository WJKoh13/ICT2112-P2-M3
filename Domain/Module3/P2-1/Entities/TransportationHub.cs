using ProRental.Domain.Enums;

namespace ProRental.Domain.Entities;

public partial class TransportationHub
{
    private HubType? _hubType;
    private HubType? HubType { get => _hubType; set => _hubType = value; }
    public void UpdateHubType(HubType newValue) => _hubType = newValue;

    public static TransportationHub Create(int hubId, string address, HubType hubType, double latitude, double longitude)
    {
        var hub = new TransportationHub();
        hub._hubId = hubId;
        hub._address = address;
        hub._hubType = hubType;
        hub._latitude = latitude;
        hub._longitude = longitude;
        hub._countryCode = "SG";
        hub._operationalStatus = "ACTIVE";
        hub._operationTime = "24/7";
        return hub;
    }

    public int ReadHubId() => _hubId;
    public string ReadAddress() => _address;
    public HubType ReadHubType() => _hubType ?? global::ProRental.Domain.Enums.HubType.WAREHOUSE;
    public double ReadLatitude() => _latitude;
    public double ReadLongitude() => _longitude;
}