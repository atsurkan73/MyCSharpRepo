using Bogus;
using LinqTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTask;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string About { get; set; }
    public List<User> Friends { get; set; }
    public List<User> UsersList { get; set; }

    public User() { }
    public User(int id, string name, double latitude, double longitude, string about, List<User> friends)
    {
        Id = id;
        Name = name;
        Latitude = latitude;
        Longitude = longitude;
        About = about;
        Friends = friends;
    }

    record CounterRecordWords(int UserId, List<string> Words, int Counter, int OtherId);
    record CounterRecordFriends(int UserId, List<string> Friends, int Counter, int OtherId);

    LinqTask.Faker faker = new LinqTask.Faker();


    public void FarthestLocated(List<User> usersForLocation)
    {
        var userNorth = usersForLocation.MaxBy(u => u.Latitude);
        var userSouth = usersForLocation.MinBy(u => u.Latitude);
        var userEast = usersForLocation.MaxBy(u => u.Longitude);
        var userWest = usersForLocation.MinBy(u => u.Longitude);



        Console.Write("User located farthest north: Id - Name - Latitude - Longitude: ");
        Console.WriteLine($"{userNorth.Id} - {userNorth.Name} - {userNorth.Latitude} - {userNorth.Longitude}");
        Console.Write("User located farthest south: Id - Name - Latitude - Longitude: ");
        Console.WriteLine($"{userSouth.Id} - {userSouth.Name} - {userSouth.Latitude} - {userSouth.Longitude}");
        Console.Write("User located farthest earth: Id - Name - Latitude - Longitude: ");
        Console.WriteLine($"{userEast.Id} - {userEast.Name} - {userEast.Latitude} - {userEast.Longitude}");
        Console.Write("User located farthest west: Id - Name - Latitude - Longitude: ");
        Console.WriteLine($"{userWest.Id} - {userWest.Name} - {userWest.Latitude} - {userWest.Longitude}");
    }

    public void DistanceBetween(List<User> usersForDistance)
    {

    double min1 = 1000000000000000;
    double max1 = 0;
    var userMin1 = usersForDistance[0];
    var userMin2 = usersForDistance[0];
    var userMax1 = usersForDistance[0];
    var userMax2 = usersForDistance[0];

for (int i = 0; i< usersForDistance.Count; i++)
{
    for (int j = i+1; j< usersForDistance.Count(); j++)
    {
        if (usersForDistance[i] != usersForDistance[j])
        {
            var distance = Math.Sqrt((usersForDistance[j].Latitude - usersForDistance[i].Latitude) * (usersForDistance[j].Latitude - usersForDistance[i].Latitude)
            + (usersForDistance[j].Longitude - usersForDistance[i].Longitude) * (usersForDistance[j].Longitude - usersForDistance[i].Longitude));

            if (distance<min1)
            {
                min1 = distance;
                userMin1 = usersForDistance[i];
                userMin2 = usersForDistance[j];
            }
            else if (distance > max1)
            {
                max1 = distance;
                userMax1 = usersForDistance[i];
                userMax2 = usersForDistance[j];
            }
        }
    }
}

Console.WriteLine($"Minimum distance {min1} is bettween {userMin1.Name} (Latitude: {userMin1.Latitude}, Longititude: {userMin1.Longitude}) " +
                 $"and {userMin2.Name} (Latitude: {userMin2.Latitude}, Longititude: {userMin2.Longitude})");

Console.WriteLine($"Maximum distance {max1} is bettween {userMax1.Name} (Latitude: {userMax1.Latitude}, Longititude: {userMax1.Longitude}) " +
                 $"and {userMax2.Name} (Latitude: {userMax2.Latitude}, Longititude: {userMax2.Longitude})"); 
}

    public void MostSameWords(List<User> usersMostSameWords)
    {
        var users2 = faker.FriendsGenerate(usersMostSameWords, 5); 

        var allusers = usersMostSameWords.Zip(users2); 

        var userdata = usersMostSameWords
            .Select(u => new CounterRecordWords(u.Id, u.About.Split(" ").Distinct().ToList(), -1, -1)).ToList();

        for (int i = 0; i < userdata.Count; i++)
        {
            var user = userdata[i];
            var otherusers = userdata.Except(new List<CounterRecordWords> { user }).ToList();

            for (int j = 0; j < otherusers.Count(); j++)
            {
                var otheruser = otherusers[j];
                var inresectCount = user.Words
                    .Intersect(otheruser.Words).Count();
                if (user.Counter < inresectCount)
                    user = user with
                    { Counter = inresectCount, OtherId = otheruser.UserId };
            }
            userdata[i] = user;
        }

        var max = userdata.MaxBy(u => u.Counter);
        Console.WriteLine($"The max intersection is between {max.UserId} and {max.OtherId} of {max.Counter} words");  // UserId and OtherId - Id of users with the most matching words in About
    }

    public void PersonsWithSameFriends(List<User> usersForSameFriends, int numFriends)
    {
        int countFriends;
        var usersWithFriends = faker.FriendsGenerate(usersForSameFriends, numFriends);

        for (int i = 0; i < usersWithFriends.Count; i++)
        {
            for (int j = i + 1; j < usersWithFriends.Count; j++)
            {
                countFriends = 0;
                var sameFriends = new List<string>();
                for (int k = 0; k < numFriends; k++)
                    if (usersWithFriends[i].Friends.Contains(usersWithFriends[j].Friends[k]))
                    {
                        countFriends++;
                        sameFriends.Add(usersWithFriends[j].Friends[k].Name);
                    }
                if (countFriends > 0)
                {
                    Console.Write($"\nUser {usersWithFriends[i].Id} has {countFriends} same friend(s) with user {usersWithFriends[j].Id}");
                    Console.Write("\nFriend name(s): ");
                    foreach (var name in sameFriends)
                        Console.Write($" {name} ");
                }
            }
        }
    }
}
