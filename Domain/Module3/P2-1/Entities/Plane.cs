namespace ProRental.Domain.Entities;

public partial class Plane
{
	public int ReadTransportId() => _transportId;

	public int ReadPlaneId() => _planeId;

	public string ReadPlaneType() => _planeType ?? string.Empty;

	public string ReadPlaneCallsign() => _planeCallsign ?? string.Empty;
}
