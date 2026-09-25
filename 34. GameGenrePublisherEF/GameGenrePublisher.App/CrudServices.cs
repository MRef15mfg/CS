using GameGenrePublisher.Data;
using GameGenrePublisher.Models;
using Microsoft.EntityFrameworkCore;

namespace GameGenrePublisher.App;

public class GameCrudService
{
    private readonly GameGenrePublisherDbContext _db;

    public GameCrudService(GameGenrePublisherDbContext db) => _db = db;

    public async Task<Game> CreateAsync(string title, decimal price, int releaseYear, string platform, string description, int publisherId, params int[] genreIds)
    {
        var game = new Game
        {
            Title = title,
            Price = price,
            ReleaseYear = releaseYear,
            Platform = platform,
            Description = description,
            PublisherId = publisherId
        };

        game.Genres = await _db.Genres.Where(g => genreIds.Contains(g.Id)).ToListAsync();
        _db.Games.Add(game);
        await _db.SaveChangesAsync();
        return game;
    }

    public Task<Game?> ReadAsync(int id) => _db.Games
        .Include(g => g.Genres)
        .FirstOrDefaultAsync(g => g.Id == id);

    public async Task<List<Game>> ReadAllAsync() => await _db.Games
        .Include(g => g.Genres)
        .ToListAsync();

    public async Task<bool> UpdateAsync(int id, string title, decimal price, int releaseYear, string platform, string description, int publisherId, params int[] genreIds)
    {
        var game = await _db.Games.Include(g => g.Genres).FirstOrDefaultAsync(g => g.Id == id);
        if (game is null) return false;

        game.Title = title;
        game.Price = price;
        game.ReleaseYear = releaseYear;
        game.Platform = platform;
        game.Description = description;
        game.PublisherId = publisherId;
        game.Genres = await _db.Genres.Where(g => genreIds.Contains(g.Id)).ToListAsync();

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var game = await _db.Games.FirstOrDefaultAsync(g => g.Id == id);
        if (game is null) return false;

        _db.Games.Remove(game);
        await _db.SaveChangesAsync();
        return true;
    }

    public Task<List<Game>> GetGamesByGenreAsync(string genreName) => _db.Games
        .Include(g => g.Genres)
        .Where(g => g.Genres.Any(genre => genre.Name == genreName))
        .ToListAsync();

    public Task<List<string>> GetGenresByGameAsync(int gameId) => _db.Games
        .Where(g => g.Id == gameId)
        .SelectMany(g => g.Genres.Select(genre => genre.Name))
        .ToListAsync();

    public async Task<List<Game>> GetGamesByPublisherAsync(int publisherId)
    {
        var publisher = await _db.Publishers.FirstOrDefaultAsync(p => p.Id == publisherId);
        if (publisher is null) return new List<Game>();

        await _db.Entry(publisher)
            .Collection(p => p.Games)
            .LoadAsync();

        return publisher.Games.ToList();
    }

    public async Task<string?> GetPublisherByGameAsync(int gameId)
    {
        var game = await _db.Games.FirstOrDefaultAsync(g => g.Id == gameId);
        if (game is null) return null;

        await _db.Entry(game)
            .Reference(g => g.Publisher)
            .LoadAsync();

        return game.Publisher.Name;
    }
}

public class GenreCrudService
{
    private readonly GameGenrePublisherDbContext _db;

    public GenreCrudService(GameGenrePublisherDbContext db) => _db = db;

    public async Task<Genre> CreateAsync(string name, string description)
    {
        var genre = new Genre { Name = name, Description = description };
        _db.Genres.Add(genre);
        await _db.SaveChangesAsync();
        return genre;
    }

    public Task<Genre?> ReadAsync(int id) => _db.Genres
        .Include(g => g.Games)
        .FirstOrDefaultAsync(g => g.Id == id);

    public async Task<List<Genre>> ReadAllAsync() => await _db.Genres
        .Include(g => g.Games)
        .ToListAsync();

    public async Task<bool> UpdateAsync(int id, string name, string description)
    {
        var genre = await _db.Genres.FirstOrDefaultAsync(g => g.Id == id);
        if (genre is null) return false;

        genre.Name = name;
        genre.Description = description;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var genre = await _db.Genres.FirstOrDefaultAsync(g => g.Id == id);
        if (genre is null) return false;

        _db.Genres.Remove(genre);
        await _db.SaveChangesAsync();
        return true;
    }
}

public class PublisherCrudService
{
    private readonly GameGenrePublisherDbContext _db;

    public PublisherCrudService(GameGenrePublisherDbContext db) => _db = db;

    public async Task<Publisher> CreateAsync(string name, string country, int foundedYear, string website, string contactEmail)
    {
        var publisher = new Publisher
        {
            Name = name,
            Country = country,
            FoundedYear = foundedYear,
            Website = website,
            ContactEmail = contactEmail
        };

        _db.Publishers.Add(publisher);
        await _db.SaveChangesAsync();
        return publisher;
    }

    public async Task<Publisher?> ReadAsync(int id)
    {
        var publisher = await _db.Publishers.FirstOrDefaultAsync(p => p.Id == id);
        if (publisher is null) return null;

        await _db.Entry(publisher)
            .Collection(p => p.Games)
            .LoadAsync();

        return publisher;
    }

    public Task<List<Publisher>> ReadAllAsync() => _db.Publishers.ToListAsync();

    public async Task<bool> UpdateAsync(int id, string name, string country, int foundedYear, string website, string contactEmail)
    {
        var publisher = await _db.Publishers.FirstOrDefaultAsync(p => p.Id == id);
        if (publisher is null) return false;

        publisher.Name = name;
        publisher.Country = country;
        publisher.FoundedYear = foundedYear;
        publisher.Website = website;
        publisher.ContactEmail = contactEmail;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var publisher = await _db.Publishers.FirstOrDefaultAsync(p => p.Id == id);
        if (publisher is null) return false;

        _db.Publishers.Remove(publisher);
        await _db.SaveChangesAsync();
        return true;
    }
}
