using LocationTracker.Data.DbContexts;
using LocationTracker.Data.IRepositories.Locations;
using LocationTracker.Domain.Entities.Locations;

namespace LocationTracker.Data.Repositories.Locations;

public class PointLocationRepository : Repository<PointLocation, int>, IPointLocationRepository
{
    public PointLocationRepository(LocationTrackerDbContext context) : 
        base(context)
    {

    }
}
