using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace WebApplicationProject.Data;

public class Customer
{
    [Key]
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Role{ get; set; }
    public string Status { get; set; }
    public string Name { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public int ServiceProfileId   { get; set; }
    public int PaymentBalance   { get; set; }
    public int PaymentSum   { get; set; }
    public string? AddInfo   { get; set; }

    public ServiceProfile ServiceProfile  { get; set; }



    public Customer() { }
    public Customer(int id, string login, string password, string role, string status, string name, string address, string phone,
                    int serviceProfileId, int paymentBalance, int paymentSum, string addInfo)
    {
        Id = id;
        Login = login;
        Password = password;
        Role = role;
        Status = status;
        Name = name;
        Address = address;
        Phone = phone;
        ServiceProfileId = serviceProfileId;
        PaymentBalance = paymentBalance;
        PaymentSum = paymentSum;
        AddInfo = addInfo;
    }

}