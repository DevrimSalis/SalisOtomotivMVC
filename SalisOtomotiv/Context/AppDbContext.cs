using Microsoft.EntityFrameworkCore;
using SalisOtomotiv.Models;

namespace SalisOtomotiv.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .Ignore(c => c.Images)
                .HasNoKey();
            modelBuilder.Entity<BodyType>()
        .HasMany(b => b.Cars)
        .WithOne(c => c.BodyType)
        .HasForeignKey(c => c.BodyTypeId);
        }

        public DbSet<BodyType> BodyTypes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<Door> Doors { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
    }
}
