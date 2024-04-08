namespace GWSales.Services.Models.Order;

public class GetOrderModel
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public DateOnly OrderDate { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAtUtc { get; set; }

    public GetOrderDetailsListModel? Details { get; set; }
}
