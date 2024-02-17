using System.ComponentModel.DataAnnotations;

namespace GWSales.Data.Entities.Product;

public class SizeEntity
{
    [Key]
    public int SizeId { get; set; }
    public string SizeRuName { get; set; }

    public List<ProductSizeEntity> Products { get; set; }
}
