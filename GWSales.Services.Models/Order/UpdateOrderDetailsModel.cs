namespace GWSales.Services.Models.Order;

public class UpdateOrderDetailsModel
{
    public int OrderDetailsId { get; set; }
    public int? Quantity { get; set; }
    public string? Comment { get; set; }
}
