using ProRental.Domain.Enums;

namespace ProRental.Domain.Entities;

public partial class DeliveryBatch
{
    private readonly List<int> _listOfOrders = [];

    private BatchStatus? _deliveryBatchStatus;
    private BatchStatus? DeliveryBatchStatus { get => _deliveryBatchStatus; set => _deliveryBatchStatus = value; }
    public void UpdateDeliveryBatchStatus(BatchStatus newValue) => _deliveryBatchStatus = newValue;

    private int getSourceHub() => _hubId ?? 0;
    private void setSourceHub(int sourceHub) => _hubId = sourceHub;

    private string getDestinationAddress() => _destinationAddress ?? string.Empty;
    private void setDestinationAddress(string destinationAddress) => _destinationAddress = destinationAddress;

    private int getDeliveryBatchIdentifier() => _deliveryBatchId;
    private void setDeliveryBatchIdentifier(int deliveryBatchIdentifier) => _deliveryBatchId = deliveryBatchIdentifier;

    private BatchStatus getDeliveryBatchStatus() => _deliveryBatchStatus ?? BatchStatus.PENDING;
    private void setDeliveryBatchStatus(BatchStatus deliveryBatchStatus) => _deliveryBatchStatus = deliveryBatchStatus;

    private int getTotalOrders() => _totalOrders ?? _listOfOrders.Count;
    private void setTotalOrders(int totalOrders) => _totalOrders = totalOrders;

    private double getCarbonSavings() => _carbonSavings ?? 0d;
    private void setCarbonSavings(double carbonSavings) => _carbonSavings = carbonSavings;

    private IReadOnlyList<int> getListOfOrders()
    {
        if (_listOfOrders.Count > 0)
        {
            return _listOfOrders;
        }

        if (BatchOrders.Count > 0)
        {
            return BatchOrders.Select(item => item.ReadOrderId()).ToList();
        }

        return _listOfOrders;
    }

    private double getBatchWeightKg() => _batchWeightKg ?? 0d;
    private void setBatchWeightKg(double weightKg) => _batchWeightKg = weightKg;

    public static DeliveryBatch Create(int batchId, int hubId, string destinationAddress)
    {
        var batch = new DeliveryBatch();
        batch.setDeliveryBatchIdentifier(batchId);
        batch.setSourceHub(hubId);
        batch.setDestinationAddress(destinationAddress);
        batch.setDeliveryBatchStatus(BatchStatus.PENDING);
        batch.setTotalOrders(0);
        batch.setCarbonSavings(0);
        batch.setBatchWeightKg(0);
        return batch;
    }

    public int ReadSourceHub() => getSourceHub();
    public string ReadDestinationAddress() => getDestinationAddress();
    public int ReadDeliveryBatchIdentifier() => getDeliveryBatchIdentifier();
    public string ReadDeliveryBatchStatus() => getDeliveryBatchStatus().ToString();
    public int ReadTotalOrders() => getTotalOrders();
    public double ReadCarbonSavings() => getCarbonSavings();
    public IReadOnlyList<int> ReadListOfOrders() => getListOfOrders();
    public double ReadBatchWeightKg() => getBatchWeightKg();

    public bool AddOrder(int orderId)
    {
        if (_listOfOrders.Contains(orderId))
        {
            return false;
        }

        _listOfOrders.Add(orderId);
        setTotalOrders(_listOfOrders.Count);
        return true;
    }

    public bool RemoveOrder(int orderId)
    {
        if (!_listOfOrders.Remove(orderId))
        {
            return false;
        }

        setTotalOrders(_listOfOrders.Count);
        return true;
    }

    public void MarkAsShipped()
    {
        setDeliveryBatchStatus(BatchStatus.SHIPPEDOUT);
    }

    public void UpdateCarbonSavings(double carbonSavings)
    {
        setCarbonSavings(carbonSavings);
    }

    public void UpdateBatchWeight(double weightKg)
    {
        setBatchWeightKg(weightKg);
    }
}
