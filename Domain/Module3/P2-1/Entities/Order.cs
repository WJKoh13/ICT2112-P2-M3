using ProRental.Domain.Enums;

namespace ProRental.Domain.Entities;

public partial class Order
{
    public static Order CreateSimulationOrder(int customerId, int checkoutId, decimal totalAmount, DateTime orderDateUtc)
    {
        var order = new Order();
        order._customerid = customerId;
        order._checkoutid = checkoutId;
        order._transactionid = null;
        order._orderdate = orderDateUtc;
        order._totalamount = totalAmount;
        order._status = OrderStatus.PENDING;
        order._deliveryType = DeliveryDuration.ThreeDays;
        return order;
    }

    public int ReadOrderId() => _orderid;
}