using Games.Models;
using Microsoft.EntityFrameworkCore;

namespace Games.Data;

public class GameDbContext : DbContext
{
    public DbSet<Game> Games { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=games.db");
        }
    }
}
