namespace GWSales.Services.Models.Product;

public class GetProductModel
{
    public int ProductId { get; set; }
    public string ArticleNumber { get; set; }
    public string ProductName { get; set; }
    public decimal WholesalePrice { get; set; }
    public decimal RetailPrice { get; set; }
    public int UnitsInStock { get; set; }
}
