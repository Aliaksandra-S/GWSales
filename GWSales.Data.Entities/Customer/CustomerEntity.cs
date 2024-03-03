using GWSales.Data.Entities.Order;
using System.ComponentModel.DataAnnotations;

namespace GWSales.Data.Entities.Customer;

public class CustomerEntity
{
    [Key]
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public int CustomerTypeId { get; set; }
    public int? DiscountId { get; set; }

    public CustomerTypeEntity CustomerType { get; set; }
    public CustomerDiscountEntity? Discount { get; set; }
    public List<OrderEntity>? Orders { get; set; }
}
