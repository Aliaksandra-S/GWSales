namespace GWSales.Services.Models.Order;

public class CreateOrderModel
{
    public int CustomerId { get; set; }
    public DateOnly OrderDate { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAtUtc { get; set; }

    public CreateOrderDetailsListModel Details { get; set; }
}
