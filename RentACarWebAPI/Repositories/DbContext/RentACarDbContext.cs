using Microsoft.EntityFrameworkCore;
using RentACarWebAPI.Models;

namespace RentACarWebAPI.Repositories
{
    public class RentACarDbContext : DbContext
    {
        public RentACarDbContext(DbContextOptions options) : base(options) { }
        
        public DbSet<Car> Cars { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasIndex(b => b.Dni)
                .IsUnique();
        }
    }
}
