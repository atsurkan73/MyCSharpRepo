using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTask;

public class Faker
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string About { get; set; }
    public List<User> Friends { get; set; }

    public Faker() { }
    public Faker(int id, string name, double latitude, double longitude,  string about, List<User> friends) 
    {
        Id = id;
        Name = name;
        Latitude = latitude;
        Longitude = longitude;
        About = about;
        Friends = friends;
    }

    public List<User> UsersGenerate(int number)
    {
        var faker = new Faker<User>()
        .RuleFor(u => u.Id, f => f.IndexFaker + 1)
        .RuleFor(u => u.Name, f => f.Name.FullName())
        .RuleFor(u => u.Latitude, f => f.Address.Latitude())
        .RuleFor(u => u.Longitude, f => f.Address.Longitude())
        .RuleFor(u => u.About, f => f.Lorem.Paragraph(10))
        .UseSeed(12345);

        var users = faker.Generate(number);
        return users;
    }

    public List<User> FriendsGenerate(List<User> usersForFriends, int numOfFriends)
    {
        var faker = new Faker<User>()
        .RuleFor(u => u.Id, f => f.IndexFaker + 1)
        .RuleFor(u => u.Friends, f => f.PickRandom(usersForFriends, numOfFriends).ToList());
        var users = faker.Generate(usersForFriends.Count).ToList();
        return users;
    }
}
