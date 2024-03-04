using System.ComponentModel.DataAnnotations;

namespace GWSales.WebApi.Models.Customer.Discount;

public class CreateDiscountDto
{
    public decimal DiscountPercentage { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Comment { get; set; }
    public List<int> CustomerIdList { get; set; }

}
