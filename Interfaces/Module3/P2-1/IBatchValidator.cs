namespace ProRental.Interfaces.Module3.P2_1;

public interface IBatchValidator
{
    bool ValidateOrderExists(string orderId);
    bool ValidateBatchExists(int batchId);
}