namespace GWSales.WebApi.Models.Order;

public class CreateOrderDetailsDto
{
    public int ProductId { get; set; }
    public int SizeId { get; set; }
    //public int ProductSizeId { get; set; }
    public int Quantity { get; set; }
    public string Comment { get; set; }
}
