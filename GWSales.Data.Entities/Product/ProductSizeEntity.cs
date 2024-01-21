using System.ComponentModel.DataAnnotations;

namespace GWSales.Data.Entities.Product;

public class ProductSizeEntity
{
    [Key]
    public int ProductSizeId { get; set; }
    public int ProductId { get; set; }
    public int SizeID { get; set; }
    public int QuantityInStock { get; set; }

    public ProductEntity Product { get; set; }
    public SizeEntity Size { get; set; }
}
