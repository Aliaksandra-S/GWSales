namespace GWSales.WebApi.Models.ProductAssortment;

public class UpdateProductDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal WholesalePrice { get; set; }
    public decimal RetailPrice { get; set; }
}
