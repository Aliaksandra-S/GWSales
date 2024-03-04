namespace GWSales.Services.Models.Customer.Discount;

public class DiscountPeriodModel
{
    public int CustomerId { get; set; }
    public DateOnly DateFrom { get; set; }
    public DateOnly DateTo { get; set; }
}
