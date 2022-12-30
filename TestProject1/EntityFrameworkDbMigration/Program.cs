/*
 * Declare models for ‘shop’ domain including:

customers
employees
products
orders
Declare db context, create migrations, try to query and save data using EF
*/



using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkDbMigration;

var user = await new NewDbContext1().Users.ToListAsync();
Console.WriteLine();
foreach (var userr in user)
    Console.WriteLine(userr.Id + " " + userr.FirstName + userr.LastName + " " + userr.Info);

var products = await new NewDbContext1().Products.ToListAsync();
Console.WriteLine();
foreach (var item in products)
    Console.WriteLine(item.ProductId + " " + item.ProductName + " " + item.Price + " $");

var orders = await new NewDbContext1().Orders.ToListAsync();
Console.WriteLine();
foreach (var item in orders)
    Console.WriteLine(item.OrderId + " " + item.ProductCode + " " + item.Samples + " (pcs)" + item.Date);


static async Task GetUsersById()
{
    await using var context = new NewDbContext1();
    var getOrder = await context.Orders.FirstOrDefaultAsync(orders => orders.UserId == 3);
    Console.WriteLine();
    Console.WriteLine("Result on getOrder query:");
    Console.WriteLine(getOrder.OrderId + " " + getOrder.ProductCode + " " + getOrder.Samples + " (pcs)" + getOrder.Date);

    var getUsers = await context.Users.Where(users => users.Id < 4).ToListAsync();

    Console.WriteLine();
    Console.WriteLine("Result on getUsers query:");
    foreach (var user in getUsers)
    Console.WriteLine(user.Id + " " + user.FirstName + user.LastName + " " + user.Info);
}

static async Task CreateOrder(int OrderId, int userId)
{
    await using var context = new NewDbContext1();

        var order = new Order
    {
        OrderId = OrderId,
        ProductCode = 3,
        UserId = userId

    };
    await context.Orders.AddAsync(order);
    await context.SaveChangesAsync();
}

static async Task DeleteOrdersByUserId(int ordreId)
{
    
    await using var context = new NewDbContext1();
    Console.WriteLine($"Order list before remove order {ordreId}");
    Console.WriteLine("OederId - ProductCode - UserId");
    foreach (var order in context.Orders)
        Console.WriteLine(order.OrderId + " " + order.ProductCode + " " + order.UserId);

    var ordersOfUser = await context.Orders.FirstOrDefaultAsync(order => order.OrderId == ordreId);
    if (ordersOfUser == null) return;
     context.Orders.Remove(ordersOfUser);
    await context.SaveChangesAsync();
    Console.WriteLine($"Order list after remove order {ordreId}");
    Console.WriteLine("OederId - ProductCode - UserId");
    foreach (var order in context.Orders)
        Console.WriteLine(order.OrderId + " " + order.ProductCode + " " + order.UserId);
}

static async Task UpdateUser(int userId)
{
    await using var context = new NewDbContext1();

    var user = await context.Users.FirstOrDefaultAsync(user => user.Id == 5);
    var updatedUser = user with { Age = 29 };
         context.Entry(user).CurrentValues.SetValues(user);
        await context.SaveChangesAsync();
    Console.WriteLine($"After update user {userId}");
    Console.WriteLine("UserId  - FullName - Age");
        Console.WriteLine(user.Id + " " + user.FirstName + user.LastName + " " + user.Age);
}

static async Task GetUserCategory()
{
    await using var context = new NewDbContext1();
    var first = await context.Users
        .Include(user => user.Category)
        .FirstOrDefaultAsync(user => user.Id == 2);
}


int orderId = 6;

await GetUsersById();

await CreateOrder(orderId, 3);

await DeleteOrdersByUserId(orderId);

await UpdateUser(5);

await GetUserCategory();



