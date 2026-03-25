namespace ProRental.Domain.Entities;

public partial class Train
{
	public int ReadTransportId() => _transportId;

	public int ReadTrainId() => _trainId;

	public string ReadTrainType() => _trainType ?? string.Empty;

	public string ReadTrainNumber() => _trainNumber ?? string.Empty;
}
