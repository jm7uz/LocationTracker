using Microsoft.EntityFrameworkCore;

namespace LocationTracker.Data.DbContexts;

public class LocationTrackerDbContext : DbContext
{
    public LocationTrackerDbContext(DbContextOptions<LocationTrackerDbContext> options) 
        : base(options)
    {
    }

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
