using Microsoft.EntityFrameworkCore;

namespace RozkladApi.Models
{
    public class DatabaseContext : DbContext
    {
    public DbSet<User> Users { get; set; }
        // DbSets for each entity in the model
        public DbSet<BusLine> BusLines { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Stop> Stops { get; set; }
        public DbSet<Departures> Departures { get; set; }

        // This method allows you to configure the model (e.g., relationships, table names, etc.)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-many relationships (or many-to-many if required)
            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.BusLine)  // Schedule has one BusLine
                .WithMany()  // BusLine has many Schedules (can be null if no navigation property in BusLine)
                .HasForeignKey(s => s.LineId)  // Foreign key in Schedule
                .OnDelete(DeleteBehavior.Restrict);  // Optional: to define delete behavior

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Stop)  // Schedule has one Stop
                .WithMany()  // Stop has many Schedules (can be null if no navigation property in Stop)
                .HasForeignKey(s => s.StopId)  // Foreign key in Schedule
                .OnDelete(DeleteBehavior.Restrict);  // Optional: to define delete behavior

            modelBuilder.Entity<Route>()
                .HasMany(r => r.Stops)  // Route has many Stops
                .WithMany()  // Stops belong to many Routes
                .UsingEntity<Dictionary<string, object>>(
                    "RouteStop",  // Join table name
                    j => j.HasOne<Stop>().WithMany().HasForeignKey("StopId"), // Foreign key for Stop
                    j => j.HasOne<Route>().WithMany().HasForeignKey("RouteId")  // Foreign key for Route
                );

            // If you want to use a custom name for the table or enforce constraints, do so here
            modelBuilder.Entity<BusLine>()
                .Property(b => b.Number)
                .HasMaxLength(50);  // Set a maximum length for BusLine Number (example)

            modelBuilder.Entity<Stop>()
                .Property(s => s.Name)
                .HasMaxLength(100);  // Set a maximum length for Stop Name (example)
        }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    }
}
