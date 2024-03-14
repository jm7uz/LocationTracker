using LocationTracker.Data.DbContexts;
using LocationTracker.Data.IRepositories.Districts;
using LocationTracker.Domain.Entities.Districts;

namespace LocationTracker.Data.Repositories.Districts;

public class DistrictRepository : Repository<District, int>, IDistrictRepository
{
    public DistrictRepository(LocationTrackerDbContext context) : 
        base(context)
    {
    }
}
