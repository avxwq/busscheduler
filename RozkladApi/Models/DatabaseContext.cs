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

        // This method allows you to configure the model (e.g., relationships, table names, etc.)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Schedule- BusLine relation (one-to-many)
            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.BusLine)  // Schedule has one BusLine
                .WithMany()  // BusLine has many Schedules
                .HasForeignKey(s => s.LineId)  // Foreign key in Schedule
                .OnDelete(DeleteBehavior.Restrict);  // Optional: to define delete behavior

            // Configure Schedule- Stop relation (one-to-many)
            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Stop)  // Schedule has one Stop
                .WithMany()  // Stop has many Schedules
                .HasForeignKey(s => s.StopId)  // Foreign key in Schedule
                .OnDelete(DeleteBehavior.Restrict);  // Optional: to define delete behavior

            // Configure Route- Stop many-to-many relation
            modelBuilder.Entity<Route>()
                .HasMany(r => r.Stops)  // Route has many Stops
                .WithMany()  // Stops belong to many Routes
                .UsingEntity<Dictionary<string, object>>(
                    "RouteStop",  // Join table name
                    j => j.HasOne<Stop>().WithMany().HasForeignKey("StopId"),  // Foreign key for Stop
                    j => j.HasOne<Route>().WithMany().HasForeignKey("RouteId")  // Foreign key for Route
                );

            // Configure Stop-Departures one-to-one relationship
            modelBuilder.Entity<Stop>()
                .HasOne(s => s.Departures)  // Stop has one Departures
                .WithOne()  // Departures has one Stop (this is optional)
                .HasForeignKey<Departures>(d => d.Id)  // Foreign key in Departures
                .OnDelete(DeleteBehavior.Cascade); // When a Stop is deleted, its Departures should also be deleted

            // Set a maximum length for BusLine Number
            modelBuilder.Entity<BusLine>()
                .Property(b => b.Number)
                .HasMaxLength(50);

            // Set a maximum length for Stop Name
            modelBuilder.Entity<Stop>()
                .Property(s => s.Name)
                .HasMaxLength(100);

            // Set max length for Departures ID
            modelBuilder.Entity<Departures>()
                .Property(d => d.Id)
                .HasMaxLength(50);  // Optional, you can adjust this as needed
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    }
}
