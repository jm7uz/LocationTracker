using AutoMapper;
using LocationTracker.Domain.Entities.Locations;
using LocationTracker.Service.DTOs.Locations.AttachedAreas;
namespace LocationTracker.Service.Mappers;

public class MappingProfile : Profile
{
    
    public MappingProfile()
    {
        //AttachedArea
        CreateMap<AttachedArea, AttachedAreaForCreationDto>();
        CreateMap<AttachedArea, AttachedAreaForResultDto>();
        CreateMap<AttachedArea, AttachedAreaForUpdateDto>();
    }
}
