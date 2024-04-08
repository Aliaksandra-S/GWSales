namespace GWSales.Services.Models.Order;

public class ChangeOrderStatusModel
{
    public int OrderId { get; set; }
    public OrderStatus OrderStatus { get; set; }
}
