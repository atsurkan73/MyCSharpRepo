using Bogus;

namespace Calendar;

public class User
{
    
    public int Id { get; set; }
    public string Name { get; set; }
    public bool RestrictedPermission { get; set; }
    public List<User> Users { get; set; }


    public User() { }
    public User(int id, string name, bool restritedPermission)
    {
        Id = id;
        Name = name;
        RestrictedPermission = restritedPermission;
    }

    public List<User> UsersGenerate(int number)
    {
        var randomizer = new Random();
        var faker = new Faker<User>()
        .RuleFor(u => u.Id, f => f.IndexFaker + 1)
        .RuleFor(u => u.Name, f => f.Name.FullName())
        .RuleFor(u => u.RestrictedPermission, f => f.PickRandom(RestrictedPermission, !RestrictedPermission))
        .UseSeed(12345);

        Users = faker.Generate(number);
        return Users;
    }

    public void PrintUsers (List<User> users)
    {
        Console.WriteLine("{Users printout: Id - Name - RestrictedPermission");
        foreach (var user in users)
            Console.WriteLine("{0} - {1} - {2}", user.Id, user.Name, user.RestrictedPermission);
    }
}


