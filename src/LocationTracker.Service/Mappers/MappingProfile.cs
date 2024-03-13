using AutoMapper;
using LocationTracker.Domain.Entities.Locations;
using LocationTracker.Service.DTOs.Locations.AttachedAreas;
using LocationTracker.Domain.Entities.Districts;
using LocationTracker.Domain.Entities.Regions;
using LocationTracker.Service.DTOs.Districts;
using LocationTracker.Service.DTOs.Regions;

namespace LocationTracker.Service.Mappers;

public class MappingProfile : Profile
{
    
    public MappingProfile()
    {
        //AttachedArea
        CreateMap<AttachedArea, AttachedAreaForCreationDto>();
        CreateMap<AttachedArea, AttachedAreaForResultDto>();
        CreateMap<AttachedArea, AttachedAreaForUpdateDto>();

        //Regions
        CreateMap<Region, RegionForCreationDto>().ReverseMap();
        CreateMap<Region, RegionForUpdateDto>().ReverseMap();
        CreateMap<Region, RegionForResultDto>().ReverseMap();

        //Districts
        CreateMap<District, DistrictForCreationDto>().ReverseMap();
        CreateMap<District, DistrictForUpdateDto>().ReverseMap();
        CreateMap<District, DistrictForResultDto>().ReverseMap();
    }
}
