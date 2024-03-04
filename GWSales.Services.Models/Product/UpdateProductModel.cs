namespace GWSales.Services.Models.Product;

public class UpdateProductModel
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public decimal? WholesalePrice { get; set; }
    public decimal? RetailPrice { get; set; }
}
