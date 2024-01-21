namespace GWSales.WebApi.Models.ProductAssortment;

public class ProductDto
{
    public string ArticleNumber { get; set; }
    public string ProductName { get; set; }
    public decimal WholesalePrice { get; set; }
    public decimal RetailPrice { get; set; }
    public int UnitsInStock { get; set; }
}
