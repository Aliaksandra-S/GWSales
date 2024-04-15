namespace GWSales.Services.Models.Product;

public class GetProductPriceListModel
{
    public List<GetProductPriceModel> Prices { get; set; } = new List<GetProductPriceModel>();
}
