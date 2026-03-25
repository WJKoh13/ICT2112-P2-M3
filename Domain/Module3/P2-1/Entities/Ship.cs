namespace ProRental.Domain.Entities;

public partial class Ship
{
	public int ReadTransportId() => _transportId;

	public int ReadShipId() => _shipId;

	public string ReadVesselType() => _vesselType ?? string.Empty;

	public string ReadVesselNumber() => _vesselNumber ?? string.Empty;

	public string ReadMaxVesselSize() => _maxVesselSize ?? string.Empty;
}
