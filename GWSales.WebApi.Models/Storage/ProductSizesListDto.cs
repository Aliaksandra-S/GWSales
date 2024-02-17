namespace GWSales.WebApi.Models.Storage;

public class ProductSizesListDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public List<GetProductSizeShortDto> ProductSizes { get; set; }
    public int TotalQuantity { get; set; }
}
