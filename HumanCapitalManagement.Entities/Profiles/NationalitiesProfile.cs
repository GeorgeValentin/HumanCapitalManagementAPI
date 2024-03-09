using AutoMapper;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.NationalityDTOs;

namespace HumanCapitalManagement.Entities.Profiles;
public class NationalitiesProfile : Profile
{
    public NationalitiesProfile()
    {
        CreateMap<ICollection<NationalityDto>, ICollection<Nationality>>().ReverseMap();
        CreateMap<NationalityDto, Nationality>().ReverseMap();
    }
}
