using GWSales.Data.Entities.Order;
using System.ComponentModel.DataAnnotations;

namespace GWSales.Data.Entities.Customer;

public class CustomerTypeEntity
{
    [Key]
    public int CustomerTypeId { get; set; }
    public string TypeName { get; set; }

    public List<CustomerEntity> Customers { get; set; }
    public List<OrderEntity> Orders { get; set; }
}
