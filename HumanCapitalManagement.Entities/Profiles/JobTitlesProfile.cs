using AutoMapper;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.JobTitleDTOs;

namespace HumanCapitalManagement.Entities.Profiles;
public class JobTitlesProfile : Profile
{
    public JobTitlesProfile()
    {
        CreateMap<JobTitle, JobTitleDto>().ReverseMap();
        CreateMap<JobTitle, JobTitleForUpdateDto>().ReverseMap();
        CreateMap<JobTitle, JobTitleForCreationDto>().ReverseMap();
        CreateMap<JobTitleDto, JobTitleForCreationDto>().ReverseMap();
        CreateMap<ICollection<JobTitle>, ICollection<JobTitleDto>>().ReverseMap();
    }
}
