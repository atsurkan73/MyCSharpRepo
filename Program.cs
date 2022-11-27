/* 
find out who is located farthest north/south/west/east using latitude/longitude data
find max and min distance between 2 persons
find 2 persons whos ‘about’ have the most same words
find persons with same friends (compare by friend’s name)
*/

using Bogus;
using LinqTask;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;

int intersectCount = 0;
var user = new User();
var faker = new LinqTask.Faker();


var users = faker.UsersGenerate(50);


user.FarthestLocated(users);

user.DistanceBetween(users);

user.MostSameWords(users);

user.PersonsWithSameFriends(users, 5);














//var list = users.OrderBy(u => u.Name);


//foreach (var item in list)
//    Console.WriteLine(item.Name);




