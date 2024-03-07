namespace GWSales.Services.Models.Customer.Discount;

public class GetDiscountWithCustomersModel
{
    public int DiscountId { get; set; }
    public decimal DiscountRate { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Comment { get; set; }

    public GetCustomerListModel? Customers { get; set; }
}
