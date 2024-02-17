namespace GWSales.WebApi.Models.Storage;

public class GetProductSizeFullDto
{
    public int ProductSizeId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int SizeId { get; set; }
    public string SizeRuName { get; set; }
    public int Quantity { get; set; }
}