namespace GWSales.Services.Models.Customer.Discount;

public class CreateDiscountModel
{
    public decimal DiscountRate { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Comment { get; set; }
}
