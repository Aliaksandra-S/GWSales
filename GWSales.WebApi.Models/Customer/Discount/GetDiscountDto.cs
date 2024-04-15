namespace GWSales.WebApi.Models.Customer.Discount;

public class GetDiscountDto
{
    public int DiscountId { get; set; }
    public decimal DiscountRate { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Comment { get; set; }
}
