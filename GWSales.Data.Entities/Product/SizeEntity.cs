using System.ComponentModel.DataAnnotations;

namespace GWSales.Data.Entities.Product;

public class SizeEntity
{
    [Key]
    public int SizeId { get; set; }
    public string SizeNameRu { get; set; }
    public string SizeNameEng { get; set; }

    public List<ProductSizeEntity> Products { get; set; }
}
