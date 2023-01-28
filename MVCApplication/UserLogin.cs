using System.ComponentModel.DataAnnotations;

namespace MVCApplication;

public class UserLogin
{
    
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }




    //public Product() { }
    //public Product(int productId, string productName, int price)
    //{
    //    ProductId = productId;
    //    ProductName = productName;
    //    Price = price;
    //}

}