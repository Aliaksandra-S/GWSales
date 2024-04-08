namespace GWSales.Services.Models.Order;

public enum OrderStatus
{
    Created,
    InProduction,
    Canceled,
    ReadyForDelivery,
    InDelivery,
    CustomerReturn,
    Completed,
}

public static class OrderStatusChangeMap
{
    public static IReadOnlyDictionary<OrderStatus, OrderStatus[]> ChangeMap { get; } = new Dictionary<OrderStatus, OrderStatus[]>
    {
        [OrderStatus.Created] = new [] {OrderStatus.Canceled, OrderStatus.InProduction, OrderStatus.ReadyForDelivery},
        [OrderStatus.InProduction] = new [] {OrderStatus.Canceled, OrderStatus.ReadyForDelivery},
        [OrderStatus.ReadyForDelivery] = new [] {OrderStatus.Canceled, OrderStatus.InProduction},
        [OrderStatus.InDelivery] = new [] {OrderStatus.CustomerReturn, OrderStatus.Completed},
        [OrderStatus.CustomerReturn] = new [] {OrderStatus.Canceled},
        [OrderStatus.Completed] = Array.Empty<OrderStatus>(),
        [OrderStatus.Canceled] = Array.Empty<OrderStatus>(),
    };
}

