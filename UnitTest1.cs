using NUnit;
using Bogus;
using LinqTask;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestPlatform.Common.Interfaces;
//using Moq;
using static LinqTask.User;
//using ConsoleApp1;

namespace TestProject1
{
    public class Tests
    {
        LinqTask.Faker faker;
        User user = new User();
        List<User> users;
        double distance;
        Queue <double> distanceQueue;
        int wordCounter;

       // Mock mock = new Mock<User>();

        User farthestNorth;
        User farthestSouth;
        User farthestEast;
        User farthestWest;
        List<User> userListForTest;



        [SetUp]
        public void Setup()
        {
           
            faker = new LinqTask.Faker();
            users = faker.UsersGenerate(50);
            User user = new User();

            var user1 = new User();
            var user2 = new User();
            var user3 = new User();
            var user4 = new User();
            user1.About = "1 aa 2 bb 3 cc 4 dd 5 ee";
            user2.About = "1 bb 2 cc 3 dd";
            user3.About = "aa bb cc dd ee ff";
            user4.About = "1 cc 2 dd 3 e";
            userListForTest = new List<User> { user1, user2, user3, user4 };

        }

        [Test(Description ="Check if user list is not null and contains required items"), Order(0)]
        public void GetUserList([Values(10, 20, 50)] int userNumber)
        {
            users = faker.UsersGenerate(userNumber);
            Assert.That(userNumber,Is.EqualTo(users.Count));
        }

        [Test(Description = "Check if farthest North user is correct"), Order(1)]
        public void CheckFarthestNorth()
        {
            user = user.FarthestLocated(users, User.Direction.North);
            Assert.That(user.Latitude, Is.EqualTo(users.Max(u => u.Latitude)));
        }


        [Test(Description = "Check if farthest South user is correct"), Order(2)]
        public void CheckFarthestSouth()
        {
            user = user.FarthestLocated(users, User.Direction.South);
            Assert.That(user.Latitude, Is.EqualTo(users.Min(u => u.Latitude)));
        }

        [Test(Description = "Check if farthest East user is correct"), Order(3)]
        public void CheckFarthestEast()
        {
            user = user.FarthestLocated(users, User.Direction.East);
            Assert.That(user.Longitude, Is.EqualTo(users.Max(u => u.Longitude)));
        }


        [Test(Description = "Check if farthest West user is correct"), Order(4)]
        public void CheckFarthestWest()
        {
            user = user.FarthestLocated(users, User.Direction.West);
            Assert.That(user.Longitude, Is.EqualTo(users.Min(u => u.Longitude)));
        }

        [Test(Description = "Check least distance calculation between users"), Order(5)]
        public void CheckGreatestDistanceCalculation([Values(10, 20, 50)] int userNumber)
        {
            users = faker.UsersGenerate(userNumber);
            distance = user.MaxDistance(users);
            distanceQueue = user.CalculateDistance(users);
          Assert.That(distance, Is.EqualTo(distanceQueue.Max()));
        }

        [Test(Description = "Check greatest distance Calculation between users"), Order(6)]
        public void CheckLeastDistanceCalculation([Values(10, 20, 50)] int userNumber)
        {
            users = faker.UsersGenerate(userNumber);
            distance = user.MinDistance(users);
            distanceQueue = user.CalculateDistance(users);
            Assert.That(distance, Is.EqualTo(distanceQueue.Min()));
        }

        [Test(Description = "Check calculation of same words in About for a pair of users"), Order(7)]
        public void CheckCalculatiofSameWordsInAbout()
        {
            var maxSameWordsCounter = user.MostSameWords(userListForTest);
            wordCounter = maxSameWordsCounter.Counter;
            Assert.That(wordCounter, Is.EqualTo(6));
        }
    }
}