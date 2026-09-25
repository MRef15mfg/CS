using GameGenrePublisher.Models;
using Microsoft.EntityFrameworkCore;

namespace GameGenrePublisher.Data;

public class GameGenrePublisherDbContext : DbContext
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Publisher> Publishers => Set<Publisher>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=game_store.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(entity =>
        {
            entity.Property(g => g.Title).IsRequired().HasMaxLength(150);
            entity.Property(g => g.Price).HasPrecision(10, 2);
            entity.Property(g => g.Platform).IsRequired().HasMaxLength(50);
            entity.Property(g => g.Description).HasMaxLength(500);

            entity.HasOne(g => g.Publisher)
                .WithMany(p => p.Games)
                .HasForeignKey(g => g.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.Property(g => g.Name).IsRequired().HasMaxLength(100);
            entity.Property(g => g.Description).HasMaxLength(300);
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.Property(p => p.Name).IsRequired().HasMaxLength(150);
            entity.Property(p => p.Country).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Website).HasMaxLength(200);
            entity.Property(p => p.ContactEmail).HasMaxLength(150);
        });

        modelBuilder.Entity<Game>()
            .HasMany(g => g.Genres)
            .WithMany(g => g.Games)
            .UsingEntity(j => j.ToTable("GameGenres"));
    }
}
