using System.ComponentModel.DataAnnotations;

namespace GWSales.Data.Entities.Customer;
public class DiscountProgramEntity
{
    [Key]
    public int DiscountId { get; set; }
    public decimal DiscountRate { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string Comment { get; set; }

    public List<CustomerEntity>? Customers { get; set; }
}
