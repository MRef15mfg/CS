using Games.Data;
using Games.Models;
using Microsoft.EntityFrameworkCore;

using var db = new GameDbContext();


db.Database.Migrate();

if (!db.Games.Any())
{
    var games = new List<Game>
{
    new()
    {
            Name = "Counter-Strike 2",
            Studio = "Valve",
            Genre = "First-person shooter",
            ReleaseDate = new DateTime(2023, 9, 27),
            Mode = GameMode.Multiplayer,
            SoldCopies = 0
        },

        new()
        {
            Name = "Dota 2",
            Studio = "Valve",
            Genre = "MOBA",
            ReleaseDate = new DateTime(2013, 7, 9),
            Mode = GameMode.Multiplayer,
            SoldCopies = 0
        },

        new()
        {
            Name = "Deadlock",
            Studio = "Valve",
            Genre = "Third-person shooter / MOBA",
            ReleaseDate = new DateTime(2024, 8, 1),
            Mode = GameMode.Multiplayer,
            SoldCopies = 0
        },

        new()
        {
            Name = "Cuphead",
            Studio = "Studio MDHR",
            Genre = "Run and gun / Platformer",
            ReleaseDate = new DateTime(2017, 9, 29),
            Mode = GameMode.Multiplayer,
            SoldCopies = 6_000_000
        }
    };

    db.Games.AddRange(games);
    db.SaveChanges();
}

Console.WriteLine("=== ІГРИ З БАЗИ ДАНИХ ===");
Console.WriteLine();

foreach (var game in db.Games.AsNoTracking().OrderBy(g => g.Id))
{
    var mode = game.Mode switch
    {
        GameMode.SinglePlayer => "однокористувацький",
        GameMode.Multiplayer => "багатокористувацький",
        _ => game.Mode.ToString()
    };

    Console.WriteLine($"ID:             {game.Id}");
    Console.WriteLine($"Назва:          {game.Name}");
    Console.WriteLine($"Студія:         {game.Studio}");
    Console.WriteLine($"Жанр:           {game.Genre}");
    Console.WriteLine($"Дата релізу:    {game.ReleaseDate:dd.MM.yyyy}");
    Console.WriteLine($"Режим:          {mode}");
    Console.WriteLine($"Продано копій:  {game.SoldCopies:N0}");
    Console.WriteLine(new string('-', 55));
}
