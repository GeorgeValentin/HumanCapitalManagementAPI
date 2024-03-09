using AutoMapper;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.SkillDTOs;

namespace HumanCapitalManagement.Entities.Profiles;
public class SkillsProfile : Profile
{
	public SkillsProfile()
	{
        CreateMap<SkillForCreationDto, Skill>().ReverseMap();
        CreateMap<SkillForCreationDto, SkillDto>().ReverseMap();
        CreateMap<SkillForUpdateDto, Skill>().ReverseMap();
        CreateMap<ICollection<SkillForCreationDto>, ICollection<Skill>>().ReverseMap();
        CreateMap<Skill, SkillDto>().ReverseMap();
    }
}
