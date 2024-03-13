using LocationTracker.Data.IRepositories.Districts;
using LocationTracker.Data.IRepositories.Locations;
using LocationTracker.Data.IRepositories.Regions;
using LocationTracker.Data.IRepositories.Users;
using LocationTracker.Data.Repositories.Districts;
using LocationTracker.Data.Repositories.Locations;
using LocationTracker.Data.Repositories.Regions;
using LocationTracker.Data.Repositories.Users;
using LocationTracker.Service.Interfaces.Locations;
using LocationTracker.Service.Services.Locations;

namespace LocationTracker.Api.Extentions;

public static class ServiceExtentions
{
    public static void AddCustomService(this IServiceCollection services)
    {
        // Users
        services.AddScoped<IUserRepository, UserRepository>();

        //Regions
        services.AddScoped<IRegionRepository, RegionRepository>();

        //Districts
        services.AddScoped<IDistrictRepository, DistrictRepository>();

        //Locations
        services.AddScoped<IAttachedAreaRepository, AttachedAreaRepository>();
        services.AddScoped<ILocationReportRepository, LocationReportRepository>();
        services.AddScoped<ILocationReportService, LocationReportService>();
        services.AddScoped<IPointLocationRepository, PointLocationRepository>();
        services.AddScoped<IUserLocationRepository, UserLocationRepository>();
    }
}
