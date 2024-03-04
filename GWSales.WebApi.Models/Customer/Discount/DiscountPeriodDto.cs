namespace GWSales.WebApi.Models.Customer.Discount;

public class DiscountPeriodDto
{
    public int CustomerId { get; set; }
    public DateOnly? DateFrom { get; set; }
    public DateOnly? DateTo { get; set; }
}
