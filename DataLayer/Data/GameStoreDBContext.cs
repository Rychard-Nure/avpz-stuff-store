using DataLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Data
{
    public class GameStoreDBContext : DbContext
    {
        public GameStoreDBContext(DbContextOptions<GameStoreDBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Game>? Games { get; set; }
        public DbSet<GameCategory>? GameCategories { get; set; }
        public DbSet<CategoryGame>? CategoryGames { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryGame>()
                .HasKey(i => new {i.GameId, i.CategoryId});

            modelBuilder.Entity<CategoryGame>()
                .HasOne(g => g.Game)
                .WithMany(c => c.Categories)
                .HasForeignKey(c => c.GameId);

            modelBuilder.Entity<CategoryGame>()
                .HasOne(g => g.Category)
                .WithMany(c => c.Games)
                .HasForeignKey(c => c.CategoryId);
        }
    }
}
