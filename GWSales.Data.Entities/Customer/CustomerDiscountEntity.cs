using System.ComponentModel.DataAnnotations;

namespace GWSales.Data.Entities.Customer;
public class CustomerDiscountEntity
{
    [Key]
    public int DiscountId { get; set; }
    public int CustomerId { get; set; }
    public decimal DiscountPercentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public CustomerEntity Customer { get; set; }
}
