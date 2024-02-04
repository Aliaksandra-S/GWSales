﻿namespace GWSales.Services.Models.Product;

public class CreateProductModel
{
    public string ArticleNumber { get; set; }
    public string ProductName { get; set; }
    public decimal WholesalePrice { get; set; }
    public decimal RetailPrice { get; set; }
}
