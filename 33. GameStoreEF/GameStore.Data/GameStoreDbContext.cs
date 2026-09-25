using GameStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data;

public class GameStoreDbContext : DbContext
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Developer> Developers => Set<Developer>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=game_store.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title).IsRequired().HasMaxLength(200);
            entity.Property(x => x.Price).IsRequired();
            entity.Property(x => x.ReleaseYear).IsRequired();

            entity.HasOne(x => x.Developer)
                .WithMany(x => x.Games)
                .HasForeignKey(x => x.DeveloperId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Developer>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).IsRequired().HasMaxLength(150);
            entity.Property(x => x.Country).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.FullName).IsRequired().HasMaxLength(150);
            entity.Property(x => x.Email).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.OrderDate).IsRequired();

            entity.HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Quantity).IsRequired();

            entity.HasOne(x => x.Order)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.Game)
                .WithMany(x => x.OrderItems)
                .HasForeignKey(x => x.GameId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
