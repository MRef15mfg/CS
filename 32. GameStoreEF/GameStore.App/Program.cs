using GameStore.Data;
using GameStore.Models;
using Microsoft.EntityFrameworkCore;

using var db = new GameStoreDbContext();

db.Database.EnsureCreated();

Seed(db);

Console.OutputEncoding = System.Text.Encoding.UTF8;

Query1(db);
Query2(db);
Query3(db);
Query4(db);
Query5(db);
Query6(db);

static void Seed(GameStoreDbContext db)
{
    if (db.Developers.Any())
        return;

    var developers = new List<Developer>
    {
        new() { Id = 1, Name = "Valve", Country = "USA" },
        new() { Id = 2, Name = "CD Projekt Red", Country = "Poland" },
        new() { Id = 3, Name = "Mojang Studios", Country = "Sweden" },
        new() { Id = 4, Name = "Studio MDHR", Country = "Canada" },
        new() { Id = 5, Name = "Larian Studios", Country = "Belgium" }
    };

    var games = new List<Game>
    {
        new() { Id = 1, Title = "Counter-Strike 2", Price = 0, ReleaseYear = 2023, DeveloperId = 1 },
        new() { Id = 2, Title = "Dota 2", Price = 0, ReleaseYear = 2013, DeveloperId = 1 },
        new() { Id = 3, Title = "Deadlock", Price = 0, ReleaseYear = 2024, DeveloperId = 1 },
        new() { Id = 4, Title = "Half-Life: Alyx", Price = 59.99, ReleaseYear = 2020, DeveloperId = 1 },
        new() { Id = 5, Title = "The Witcher 3", Price = 39.99, ReleaseYear = 2015, DeveloperId = 2 },
        new() { Id = 6, Title = "Cyberpunk 2077", Price = 59.99, ReleaseYear = 2020, DeveloperId = 2 },
        new() { Id = 7, Title = "Minecraft", Price = 29.99, ReleaseYear = 2011, DeveloperId = 3 },
        new() { Id = 8, Title = "Cuphead", Price = 19.99, ReleaseYear = 2017, DeveloperId = 4 },
        new() { Id = 9, Title = "Baldur's Gate 3", Price = 59.99, ReleaseYear = 2023, DeveloperId = 5 },
        new() { Id = 10, Title = "Divinity: Original Sin 2", Price = 44.99, ReleaseYear = 2017, DeveloperId = 5 }
    };

    var customers = new List<Customer>
    {
        new() { Id = 1, FullName = "Andrii Melnyk", Email = "andrii@example.com" },
        new() { Id = 2, FullName = "Olena Kovalenko", Email = "olena@example.com" },
        new() { Id = 3, FullName = "Maksym Bondarenko", Email = "maksym@example.com" },
        new() { Id = 4, FullName = "Iryna Shevchenko", Email = "iryna@example.com" },
        new() { Id = 5, FullName = "Dmytro Tkachenko", Email = "dmytro@example.com" },
        new() { Id = 6, FullName = "Sofia Marchenko", Email = "sofia@example.com" },
        new() { Id = 7, FullName = "Artem Kravchenko", Email = "artem@example.com" },
        new() { Id = 8, FullName = "Viktoriia Boyko", Email = "viktoriia@example.com" }
    };

    var orders = new List<Order>
    {
        new() { Id = 1, CustomerId = 1, OrderDate = new DateTime(2026, 1, 10) },
        new() { Id = 2, CustomerId = 1, OrderDate = new DateTime(2026, 1, 18) },
        new() { Id = 3, CustomerId = 2, OrderDate = new DateTime(2026, 2, 2) },
        new() { Id = 4, CustomerId = 3, OrderDate = new DateTime(2026, 2, 15) },
        new() { Id = 5, CustomerId = 4, OrderDate = new DateTime(2026, 3, 1) },
        new() { Id = 6, CustomerId = 4, OrderDate = new DateTime(2026, 3, 12) },
        new() { Id = 7, CustomerId = 5, OrderDate = new DateTime(2026, 4, 7) },
        new() { Id = 8, CustomerId = 6, OrderDate = new DateTime(2026, 4, 20) },
        new() { Id = 9, CustomerId = 7, OrderDate = new DateTime(2026, 5, 5) },
        new() { Id = 10, CustomerId = 8, OrderDate = new DateTime(2026, 5, 22) }
    };

    var orderItems = new List<OrderItem>
    {
        new() { Id = 1, OrderId = 1, GameId = 5, Quantity = 1 },
        new() { Id = 2, OrderId = 1, GameId = 8, Quantity = 2 },
        new() { Id = 3, OrderId = 2, GameId = 6, Quantity = 1 },
        new() { Id = 4, OrderId = 2, GameId = 7, Quantity = 1 },
        new() { Id = 5, OrderId = 3, GameId = 9, Quantity = 1 },
        new() { Id = 6, OrderId = 3, GameId = 10, Quantity = 1 },
        new() { Id = 7, OrderId = 4, GameId = 4, Quantity = 1 },
        new() { Id = 8, OrderId = 4, GameId = 1, Quantity = 1 },
        new() { Id = 9, OrderId = 5, GameId = 2, Quantity = 3 },
        new() { Id = 10, OrderId = 5, GameId = 8, Quantity = 1 },
        new() { Id = 11, OrderId = 6, GameId = 9, Quantity = 2 },
        new() { Id = 12, OrderId = 6, GameId = 3, Quantity = 1 },
        new() { Id = 13, OrderId = 7, GameId = 10, Quantity = 1 },
        new() { Id = 14, OrderId = 7, GameId = 5, Quantity = 1 },
        new() { Id = 15, OrderId = 8, GameId = 6, Quantity = 1 },
        new() { Id = 16, OrderId = 8, GameId = 1, Quantity = 2 },
        new() { Id = 17, OrderId = 9, GameId = 4, Quantity = 1 },
        new() { Id = 18, OrderId = 9, GameId = 7, Quantity = 1 },
        new() { Id = 19, OrderId = 10, GameId = 9, Quantity = 1 },
        new() { Id = 20, OrderId = 10, GameId = 8, Quantity = 2 }
    };

    db.Developers.AddRange(developers);
    db.Games.AddRange(games);
    db.Customers.AddRange(customers);
    db.Orders.AddRange(orders);
    db.OrderItems.AddRange(orderItems);
    db.SaveChanges();
}

static void Query1(GameStoreDbContext db)
{
    Console.WriteLine("1. Всі ігри з розробниками");
    Console.WriteLine(new string('=', 70));

    var games = db.Games
        .Include(g => g.Developer)
        .OrderBy(g => g.Id)
        .ToList();

    foreach (var game in games)
        Console.WriteLine($"{game.Title,-28} | {game.Developer.Name,-20} | {game.Price,6:F2}$ | {game.ReleaseYear}");

    Console.WriteLine();
}

static void Query2(GameStoreDbContext db)
{
    Console.WriteLine("2. Замовлення з клієнтами та іграми");
    Console.WriteLine(new string('=', 70));

    var orders = db.Orders
        .Include(o => o.Customer)
        .Include(o => o.OrderItems)
        .ThenInclude(i => i.Game)
        .OrderBy(o => o.Id)
        .ToList();

    foreach (var order in orders)
    {
        Console.WriteLine($"Замовлення #{order.Id}, {order.OrderDate:dd.MM.yyyy}, клієнт: {order.Customer.FullName}");
        foreach (var item in order.OrderItems)
            Console.WriteLine($"  - {item.Game.Title}, кількість: {item.Quantity}");
    }

    Console.WriteLine();
}

static void Query3(GameStoreDbContext db)
{
    Console.WriteLine("3. Сума кожного замовлення");
    Console.WriteLine(new string('=', 70));

    var orders = db.Orders
        .Include(o => o.Customer)
        .Include(o => o.OrderItems)
        .ThenInclude(i => i.Game)
        .OrderBy(o => o.Id)
        .ToList();

    foreach (var order in orders)
    {
        var total = order.OrderItems.Sum(i => i.Quantity * i.Game.Price);
        Console.WriteLine($"Замовлення #{order.Id} | {order.Customer.FullName,-20} | {total:F2}$");
    }

    Console.WriteLine();
}

static void Query4(GameStoreDbContext db)
{
    Console.WriteLine("4. Топ 3 найдорожчі ігри");
    Console.WriteLine(new string('=', 70));

    var games = db.Games
        .OrderByDescending(g => g.Price)
        .Take(3)
        .Include(g => g.Developer)
        .ToList();

    foreach (var game in games)
        Console.WriteLine($"{game.Title} | {game.Price:F2}$ | {game.Developer.Name}");

    Console.WriteLine();
}

static void Query5(GameStoreDbContext db)
{
    Console.WriteLine("5. Клієнти з більш ніж 1 замовленням");
    Console.WriteLine(new string('=', 70));

    var customers = db.Customers
        .Where(c => c.Orders.Count > 1)
        .Select(c => new
        {
            c.FullName,
            c.Email,
            OrdersCount = c.Orders.Count
        })
        .OrderByDescending(c => c.OrdersCount)
        .ToList();

    foreach (var customer in customers)
        Console.WriteLine($"{customer.FullName,-20} | {customer.Email,-28} | Замовлень: {customer.OrdersCount}");

    Console.WriteLine();
}

static void Query6(GameStoreDbContext db)
{
    Console.WriteLine("6. Загальний дохід магазину");
    Console.WriteLine(new string('=', 70));

    var totalRevenue = db.OrderItems
        .Include(i => i.Game)
        .AsEnumerable()
        .Sum(i => i.Quantity * i.Game.Price);

    Console.WriteLine($"Загальний дохід: {totalRevenue:F2}$");
}
