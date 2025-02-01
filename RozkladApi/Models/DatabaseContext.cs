using Microsoft.EntityFrameworkCore;

namespace RozkladApi.Models
{
    public class DatabaseContext : DbContext
    {
    public DbSet<User> Users { get; set; }

    public DbSet<BusLine> BusLines { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Stop> Stops { get; set; }
    public DbSet<Departures> Departures { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Schedule>()
            .HasOne<BusLine>()
            .WithMany()
            .HasForeignKey(s => s.LineId);

        modelBuilder.Entity<Schedule>()
            .HasOne<Stop>()
            .WithMany()
            .HasForeignKey(s => s.StopId);
    }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    }
}
