namespace GWSales.WebApi.Models.Order;

public class UpdateOrderDetailsDto
{ 
    public int OrderDetailsId { get; set; }
    public int? Quantity { get; set; }
    public string? Comment { get; set; }
}
