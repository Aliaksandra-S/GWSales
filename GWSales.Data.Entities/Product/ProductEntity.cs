using System.ComponentModel.DataAnnotations;

namespace GWSales.Data.Entities.Product;

public class ProductEntity
{
    [Key]
    public int ProductId { get; set; }
    public string ArticleNumber { get; set; }
    public string ProductName { get; set; }
    public decimal WholesalePrice { get; set; }
    public decimal RetailPrice { get; set; }
    public int UnitsInStock { get; set; }
    public bool IsDeleted { get; set; }

    public List<ProductSizeEntity>? Sizes { get; set; }
}
