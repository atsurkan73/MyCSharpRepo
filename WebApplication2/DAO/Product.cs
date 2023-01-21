using System.ComponentModel.DataAnnotations;

namespace WebApiApplication.DAO;

public class Product
{
    [Key]
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Price { get; set; }
    public string Category { get; set; }

    public ProductCategory ProdCategory { get; set; }



    public Product() { }
    public Product(int productId, string productName, int price, string category)
    {
        ProductId = productId;
        ProductName = productName;
        Price = price;
        Category = category;
    }

}