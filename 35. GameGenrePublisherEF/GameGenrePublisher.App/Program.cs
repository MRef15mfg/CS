using GameGenrePublisher.Data;
using GameGenrePublisher.Models;
using GameGenrePublisher.App;

using var db = new GameGenrePublisherDbContext();
await db.Database.EnsureCreatedAsync();

if (!db.Games.Any())
{
    var publishersToSeed = new List<Publisher>
    {
        new() { Name = "Valve", Country = "USA", FoundedYear = 1996, Website = "https://www.valvesoftware.com", ContactEmail = "contact@valvesoftware.com" },
        new() { Name = "CD Projekt Red", Country = "Poland", FoundedYear = 2002, Website = "https://www.cdprojektred.com", ContactEmail = "contact@cdprojektred.com" },
        new() { Name = "Nintendo", Country = "Japan", FoundedYear = 1889, Website = "https://www.nintendo.com", ContactEmail = "contact@nintendo.com" },
        new() { Name = "Electronic Arts", Country = "USA", FoundedYear = 1982, Website = "https://www.ea.com", ContactEmail = "contact@ea.com" }
    };

    var genresToSeed = new List<Genre>
    {
        new() { Name = "Action", Description = "Action-oriented games" },
        new() { Name = "RPG", Description = "Role-playing games" },
        new() { Name = "Shooter", Description = "Shooter games" },
        new() { Name = "Adventure", Description = "Adventure games" },
        new() { Name = "Platformer", Description = "Platform games" },
        new() { Name = "Sports", Description = "Sports games" }
    };

    db.Publishers.AddRange(publishersToSeed);
    db.Genres.AddRange(genresToSeed);
    await db.SaveChangesAsync();

    var gamesToSeed = new List<Game>
    {
        new() { Title = "Counter-Strike 2", Price = 0m, ReleaseYear = 2023, Platform = "PC", Description = "Competitive multiplayer shooter", PublisherId = publishersToSeed[0].Id, Genres = new[] { genresToSeed[0], genresToSeed[2] } },
        new() { Title = "Dota 2", Price = 0m, ReleaseYear = 2013, Platform = "PC", Description = "Multiplayer online battle arena", PublisherId = publishersToSeed[0].Id, Genres = new[] { genresToSeed[0], genresToSeed[2] } },
        new() { Title = "Half-Life 2", Price = 9.99m, ReleaseYear = 2004, Platform = "PC", Description = "Story-driven first-person shooter", PublisherId = publishersToSeed[0].Id, Genres = new[] { genresToSeed[0], genresToSeed[2] } },
        new() { Title = "The Witcher 3", Price = 39.99m, ReleaseYear = 2015, Platform = "PC", Description = "Open-world role-playing game", PublisherId = publishersToSeed[1].Id, Genres = new[] { genresToSeed[0], genresToSeed[1], genresToSeed[3] } },
        new() { Title = "Cyberpunk 2077", Price = 49.99m, ReleaseYear = 2020, Platform = "PC", Description = "Open-world action RPG", PublisherId = publishersToSeed[1].Id, Genres = new[] { genresToSeed[0], genresToSeed[1] } },
        new() { Title = "The Legend of Zelda: Breath of the Wild", Price = 59.99m, ReleaseYear = 2017, Platform = "Switch", Description = "Open-world adventure", PublisherId = publishersToSeed[2].Id, Genres = new[] { genresToSeed[0], genresToSeed[3] } },
        new() { Title = "Super Mario Odyssey", Price = 59.99m, ReleaseYear = 2017, Platform = "Switch", Description = "3D platformer", PublisherId = publishersToSeed[2].Id, Genres = new[] { genresToSeed[0], genresToSeed[4] } },
        new() { Title = "EA Sports FC 25", Price = 69.99m, ReleaseYear = 2024, Platform = "PC", Description = "Football sports game", PublisherId = publishersToSeed[3].Id, Genres = new[] { genresToSeed[5] } },
        new() { Title = "Dead Space", Price = 59.99m, ReleaseYear = 2023, Platform = "PC", Description = "Science-fiction survival horror", PublisherId = publishersToSeed[3].Id, Genres = new[] { genresToSeed[0], genresToSeed[3] } },
        new() { Title = "Apex Legends", Price = 0m, ReleaseYear = 2019, Platform = "PC", Description = "Battle royale shooter", PublisherId = publishersToSeed[3].Id, Genres = new[] { genresToSeed[0], genresToSeed[2] } }
    };

    db.Games.AddRange(gamesToSeed);
    await db.SaveChangesAsync();
}

var gameCrud = new GameCrudService(db);
var genreCrud = new GenreCrudService(db);
var publisherCrud = new PublisherCrudService(db);

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("=== Games / Genres / Publishers ===");
Console.WriteLine();

Console.WriteLine("1. Всі ігри та їх жанри (eager loading)");
foreach (var game in await gameCrud.ReadAllAsync())
{
    Console.WriteLine($"{game.Id}. {game.Title} | {string.Join(", ", game.Genres.Select(g => g.Name))}");
}

Console.WriteLine();
Console.WriteLine("2. Жанри вказаної гри (eager loading)");
var selectedGame = await gameCrud.ReadAsync(4);
Console.WriteLine($"{selectedGame?.Title}: {string.Join(", ", selectedGame?.Genres.Select(g => g.Name) ?? Enumerable.Empty<string>())}");

Console.WriteLine();
Console.WriteLine("3. Всі ігри видавця (explicit loading)");
var publisherGames = await gameCrud.GetGamesByPublisherAsync(1);
foreach (var game in publisherGames)
{
    Console.WriteLine($"- {game.Title}");
}

Console.WriteLine();
Console.WriteLine("4. Всі ігри вказаного жанру (eager loading)");
var actionGames = await gameCrud.GetGamesByGenreAsync("Action");
foreach (var game in actionGames)
{
    Console.WriteLine($"- {game.Title}");
}

Console.WriteLine();
Console.WriteLine("5. Видавець вказаної гри (explicit loading)");
Console.WriteLine($"The Witcher 3: {await gameCrud.GetPublisherByGameAsync(4)}");

Console.WriteLine();
Console.WriteLine("6. CRUD методи");
var testGenre = await genreCrud.CreateAsync("Test Genre", "Temporary genre");
var testGame = await gameCrud.CreateAsync("Test Game", 19.99m, 2026, "PC", "Temporary game", 1, testGenre.Id);
var testPublisher = await publisherCrud.CreateAsync("Test Publisher", "USA", 2026, "https://example.com", "test@example.com");

var readGenre = await genreCrud.ReadAsync(testGenre.Id);
var readGame = await gameCrud.ReadAsync(testGame.Id);
var readPublisher = await publisherCrud.ReadAsync(testPublisher.Id);

Console.WriteLine($"Create + Read Genre: {readGenre?.Name}");
Console.WriteLine($"Create + Read Game: {readGame?.Title}");
Console.WriteLine($"Create + Read Publisher: {readPublisher?.Name}");

await genreCrud.UpdateAsync(testGenre.Id, "Updated Genre", "Updated description");
await gameCrud.UpdateAsync(testGame.Id, "Updated Game", 29.99m, 2026, "PC", "Updated description", 1, testGenre.Id);
await publisherCrud.UpdateAsync(testPublisher.Id, "Updated Publisher", "UK", 2025, "https://example.org", "updated@example.org");

Console.WriteLine($"Update Genre: {(await genreCrud.ReadAsync(testGenre.Id))?.Name}");
Console.WriteLine($"Update Game: {(await gameCrud.ReadAsync(testGame.Id))?.Title}");
Console.WriteLine($"Update Publisher: {(await publisherCrud.ReadAsync(testPublisher.Id))?.Name}");

await gameCrud.DeleteAsync(testGame.Id);
await genreCrud.DeleteAsync(testGenre.Id);
await publisherCrud.DeleteAsync(testPublisher.Id);

Console.WriteLine("Delete Game: виконано");
Console.WriteLine("Delete Genre: виконано");
Console.WriteLine("Delete Publisher: виконано");
