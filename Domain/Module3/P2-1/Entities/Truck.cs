namespace ProRental.Domain.Entities;

public partial class Truck
{
	public int ReadTransportId() => _transportId;

	public int ReadTruckId() => _truckId;

	public string ReadTruckType() => _truckType ?? string.Empty;

	public string ReadLicensePlate() => _licensePlate ?? string.Empty;
}
