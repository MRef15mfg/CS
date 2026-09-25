namespace Games.Models;

public enum GameMode
{
    SinglePlayer,
    Multiplayer
}

public class Game
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Studio { get; set; } = string.Empty;

    public string Genre { get; set; } = string.Empty;

    public DateTime ReleaseDate { get; set; }

    public GameMode Mode { get; set; }

    public long SoldCopies { get; set; }
}
