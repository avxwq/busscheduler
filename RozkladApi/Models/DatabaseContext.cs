using Microsoft.EntityFrameworkCore;

namespace RozkladApi.Models
{
    public class DatabaseContext : DbContext
    {
    public DbSet<User> Users { get; set; }
    public DbSet<BusStop> BusStops { get; set; }
    public DbSet<BusLine> BusLines { get; set; }
   public DbSet<BusLineStop> BusLineStops { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BusLineStop>()
                .HasOne(bls => bls.BusLine)
                .WithMany(bl => bl.LineStops)
                .HasForeignKey(bls => bls.BusLineId);

            modelBuilder.Entity<BusLineStop>()
                .HasOne(bls => bls.BusStop)
                .WithMany()
                .HasForeignKey(bls => bls.BusStopId);

            modelBuilder.Entity<BusLineStop>()
                .HasOne(bls => bls.BusDepartureStop)
                .WithMany()
                .HasForeignKey(bls => bls.BusDepartureStopId);

        }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    }
}
