namespace GWSales.WebApi.Models.Order;

public class CreateOrderDto
{
    public int CustomerId { get; set; }
    public DateOnly OrderDate { get; set; }

    public CreateOrderDetailsListDto Details { get; set; }
}
