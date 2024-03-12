using LocationTracker.Data.DbContexts;
using LocationTracker.Data.IRepositories.Regions;
using LocationTracker.Domain.Entities.Regions;

namespace LocationTracker.Data.Repositories.Regions;

public class RegionRepository : Repository<Region, int>, IRegionRepository
{
    public RegionRepository(LocationTrackerDbContext context) : 
        base(context)
    {

    }
}
