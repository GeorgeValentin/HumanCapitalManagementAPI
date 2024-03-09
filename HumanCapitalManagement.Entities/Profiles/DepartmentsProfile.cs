using AutoMapper;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.DepartmentDTOs;

namespace HumanCapitalManagement.Entities.Profiles;
public class DepartmentsProfile : Profile
{
    public DepartmentsProfile()
    {
        CreateMap<ICollection<DepartmentDto>, ICollection<Department>>().ReverseMap();
        CreateMap<DepartmentDto, Department>().ReverseMap();
    }
}
