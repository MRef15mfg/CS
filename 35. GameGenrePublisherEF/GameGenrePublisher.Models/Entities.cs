namespace GameGenrePublisher.Models;

public class Game
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int ReleaseYear { get; set; }
    public string Platform { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int PublisherId { get; set; }
    public Publisher Publisher { get; set; } = null!;
    public ICollection<Genre> Genres { get; set; } = new List<Genre>();
}

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<Game> Games { get; set; } = new List<Game>();
}

public class Publisher
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public int FoundedYear { get; set; }
    public string Website { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public ICollection<Game> Games { get; set; } = new List<Game>();
}
