using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DatabaseManager
{
    public class LudoDbContext : DbContext
    {

        public DbSet<Player> players { get; set; }
        public DbSet<Game> games { get; set; }
        public DbSet<Pawn> pawns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>();        
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("Appsettings.Json")
                .Build();

            var defaultConnection = configuration.GetConnectionString("DefaultConnection");
            optionBuilder.UseSqlServer(defaultConnection);
        }

    }
}