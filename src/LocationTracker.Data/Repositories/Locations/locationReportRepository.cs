using LocationTracker.Data.DbContexts;
using LocationTracker.Data.IRepositories.Locations;
using LocationTracker.Domain.Entities.Locations;

namespace LocationTracker.Data.Repositories.Locations;

public class LocationReportRepository : Repository<locationReport, long>, ILocationReportRepository
{
    public LocationReportRepository(LocationTrackerDbContext context) : 
        base(context)
    {

    }
}
