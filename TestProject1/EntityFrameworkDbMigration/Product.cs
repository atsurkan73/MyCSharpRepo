﻿
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkDbMigration;

public class Product
{
    [Key]
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Price { get; set; }
    

    public Product() { }
    public Product(int productId, string productName, int price)
    {
        ProductId = productId;
        ProductName = productName;
        Price = price;
    }
                
}
