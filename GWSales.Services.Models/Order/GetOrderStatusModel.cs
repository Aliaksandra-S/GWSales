namespace GWSales.Services.Models.Order;

public class GetOrderStatusModel
{
    public int OrderId { get; set; }
    public OrderStatus OrderStatus { get; set; }
}
