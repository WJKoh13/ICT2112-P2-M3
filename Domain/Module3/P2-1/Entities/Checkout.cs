namespace ProRental.Domain.Entities;

public partial class Checkout
{
    public int ReadCheckoutId() => _checkoutid;
    public int ReadCustomerId() => _customerid;
}