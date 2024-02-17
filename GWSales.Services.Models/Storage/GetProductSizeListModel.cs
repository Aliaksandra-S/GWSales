namespace GWSales.Services.Models.Storage;

public class GetProductSizeListModel
{
    public int ProductId { get; set; }
    public List<GetProductSizeModel> ProductSizes { get; set; }

}
