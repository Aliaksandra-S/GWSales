namespace GWSales.WebApi.Models.Order;

public class GetOrderDetailsDto
{
    public int OrderDetailsId { get; set; }
    public int ProductSizeId { get; set; }
    public int Quantity { get; set; }
    public decimal SubtotalAmount { get; set; }
    public decimal AppliedDiscountRate { get; set; }
    public string Comment { get; set; }
}
