using GWSales.Data.Entities.Customer;
using GWSales.Services.Models.Order;
using System.ComponentModel.DataAnnotations;

namespace GWSales.Data.Entities.Order;

public class OrderEntity
{
    [Key]
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public int CustomerTypeId { get; set; }
    public DateOnly OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime CreatedAtUtc { get; set; }


    public CustomerEntity Customer { get; set; }
    public CustomerTypeEntity CustomerType { get; set; }
    public List<OrderDetailsEntity> OrderDetails { get; set; }
}
