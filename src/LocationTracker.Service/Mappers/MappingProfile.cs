using AutoMapper;
using LocationTracker.Domain.Entities.Locations;
using LocationTracker.Domain.Entities.Users;
using LocationTracker.Service.DTOs.Locations.AttachedAreas;
using LocationTracker.Service.DTOs.Users;
using LocationTracker.Domain.Entities.Districts;
using LocationTracker.Domain.Entities.Regions;
using LocationTracker.Service.DTOs.Districts;
using LocationTracker.Service.DTOs.Regions;
using LocationTracker.Service.DTOs.Locations.LocationReports;
using LocationTracker.Service.DTOs.Locations.UserLocations;

namespace LocationTracker.Service.Mappers;

public class MappingProfile : Profile
{
    
    public MappingProfile()
    {
        //User
        CreateMap<User, UserForResultDto>().ReverseMap();
        CreateMap<User, UserForCreationDto>().ReverseMap();
        CreateMap<User, UserForUpdateDto>().ReverseMap();

        //AttachedArea
        CreateMap<AttachedArea, AttachedAreaForCreationDto>().ReverseMap();
        CreateMap<AttachedArea, AttachedAreaForResultDto>().ReverseMap();
        CreateMap<AttachedArea, AttachedAreaForUpdateDto>().ReverseMap();

        // LocationReport
        CreateMap<locationReport, LocationReportForCreationDto>().ReverseMap();
        CreateMap<locationReport, LocationReportForUpdateDto>().ReverseMap();
        CreateMap<locationReport, LocationReportForResultDto>().ReverseMap();
        // Regions
        CreateMap<Region, RegionForCreationDto>().ReverseMap();
        CreateMap<Region, RegionForUpdateDto>().ReverseMap();
        CreateMap<Region, RegionForResultDto>().ReverseMap();

        // Districts
        CreateMap<District, DistrictForCreationDto>().ReverseMap();
        CreateMap<District, DistrictForUpdateDto>().ReverseMap();
        CreateMap<District, DistrictForResultDto>().ReverseMap();

        // User Location
        CreateMap<UserLocation, UserLocationForCreationDto>().ReverseMap();
        CreateMap<UserLocation, UserLocationForResultDto>().ReverseMap();
        CreateMap<UserLocation, UserLocationForUpdateDto>().ReverseMap();
    }

}
