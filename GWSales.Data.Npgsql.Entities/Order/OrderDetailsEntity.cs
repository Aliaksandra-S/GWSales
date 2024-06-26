﻿using GWSales.Data.Entities.Product;
using System.ComponentModel.DataAnnotations;

namespace GWSales.Data.Entities.Order;

public class OrderDetailsEntity
{
    [Key]
    public int OrderDetailsId { get; set; }
    public int OrderId { get; set; }
    public int ProductSizeId { get; set; }
    public int Quantity { get; set; }
    public decimal SubtotalAmount { get; set; }
    public decimal AppliedDiscountRate { get; set; }
    public string Comment { get; set; }

    public OrderEntity Order { get; set; }
    public ProductSizeEntity ProductSize { get; set; }
}
