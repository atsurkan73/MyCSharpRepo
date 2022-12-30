
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestForEntityFramework;

namespace TestForEntityFramework;

public record User
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Info { get; set; }
    public int Age { get; set; }
    public string Sex { get; set; }
    public string Address { get; set; }
    public UserCategory Category { get; set; }

    

    public User() { }
    public User(int id, string firstName, string lastName, string info, int age, string sex, string address)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Info = info;
        Age = age;
        Sex = sex;
        Address = address;

    }

    //public async IAsyncEnumerable <User>  GetUsers()
    //{
    //    Users = await new NewDbContext().Users.ToListAsync();
    //     return Users;

    //}

    //public void PrintUsers(List <User> userList)
    //{
    //    foreach (var user in userList)
    //        Console.WriteLine(user.Id + " " + user.FirstName + user.LastName + " " + user.Info);

    //}

                
}
