using LocationTracker.Data.DbContexts;
using LocationTracker.Data.IRepositories.Locations;
using LocationTracker.Domain.Entities.Locations;

namespace LocationTracker.Data.Repositories.Locations;

public class UserLocationRepository : Repository<UserLocation, long>, IUserLocationRepository
{
    public UserLocationRepository(LocationTrackerDbContext context) : base(context)
    {

    }
}
