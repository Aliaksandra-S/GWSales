namespace GWSales.Services.Models.Order;

public class GetOrderDetailsModel
{
    public int OrderDetailsId { get; set; }
    public int OrderId { get; set; }
    public int ProductSizeId { get; set; }
    public int Quantity { get; set; }
    public decimal SubtotalAmount { get; set; }
    public decimal AppliedDiscountRate { get; set; }
    public string Comment { get; set; }
}
