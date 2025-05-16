namespace App.Domain.enums;

public enum OrderStatus
{
    Pending,
    AwaitingPickup,
    InDelivery,
    Completed,
    Cancelled,
    Refunded,
    Declined
}