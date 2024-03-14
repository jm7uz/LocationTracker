using LocationTracker.Domain.Entities.Districts;
using LocationTracker.Domain.Entities.Locations;
using LocationTracker.Domain.Entities.Regions;
using LocationTracker.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace LocationTracker.Data.DbContexts;

public class LocationTrackerDbContext : DbContext
{
    public LocationTrackerDbContext(DbContextOptions<LocationTrackerDbContext> options) 
        : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<locationReport> LocationReports { get; set; }
    public DbSet<AttachedArea> AttachedAreas { get; set; }
    public DbSet<UserLocation> UserLocations { get; set; }
    public DbSet<PointLocation> PointLocations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Task.Run(() =>
        {
            SeedUsers(modelBuilder);
        }).Wait();
    }
    private void SeedUsers(ModelBuilder modelBuilder)
    {

    }
}
