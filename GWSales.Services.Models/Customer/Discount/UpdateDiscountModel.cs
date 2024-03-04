namespace GWSales.Services.Models.Customer.Discount;

public class UpdateDiscountModel
{
    public int DiscountId { get; set; }
    public decimal? DiscountPercentage { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Comment { get; set; }
}
