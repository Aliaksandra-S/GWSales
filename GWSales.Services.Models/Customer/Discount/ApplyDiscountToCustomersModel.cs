namespace GWSales.Services.Models.Customer.Discount;

public class ApplyDiscountToCustomersModel
{
    public int DiscountId { get; set; }
    public List<int> CustomerIdList { get; set; }
}
