using System.ComponentModel.DataAnnotations;

namespace GWSales.Data.Entities.Product;

public class ProductSizeEntity
{
    [Key]
    public int ProductSizeId { get; set; }
    public int ProductId { get; set; }
    public int SizeId { get; set; }
    public int Quantity { get; set; }

    public ProductEntity Product { get; set; }
    public SizeEntity Size { get; set; }
}
