using System.ComponentModel.DataAnnotations;

namespace WebApplicationProject.Data;

public class TariffPlan
{
    [Key]
    public string TariffName { get; set; }
    public int? Speed{ get; set; }
    public string? TrafficLimit { get; set; }
    public string? IPTV { get; set; }
    public int TariffId { get; set; }

    public ServiceProfile ServiceProfile { get; set; }



    //public Customers() { }
    //public Customers(int productId, string productName, int price)
    //{
    //    ProductId = productId;
    //    ProductName = productName;
    //    Price = price;
    //}

}