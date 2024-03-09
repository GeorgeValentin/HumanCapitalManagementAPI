using AutoMapper;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.AddressDTOs;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;

namespace HumanCapitalManagement.Entities.Profiles;
public class EmployeesProfile : Profile
{
    public EmployeesProfile()
    {
        CreateMap<AddressDto, Address>().ReverseMap();
        CreateMap<AddressForCreationValidatorDto, Address>().ReverseMap();
        CreateMap<Employee, EmployeeDto>().ReverseMap();
        CreateMap<EmployeeForCreationDto, Employee>()
            .ForMember(dest => dest.Id,
                       option => option.MapFrom(src => 0))
            .ForMember(dest => dest.IsDeleted,
                       option => option.MapFrom(src => false)).ReverseMap();
        CreateMap<EmployeeForCreationDto, EmployeeDto>().ReverseMap();
        CreateMap<EmployeeForUpdateDto, EmployeeDto>().ReverseMap();
        CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
        CreateMap<EmployeeForCreationValidatorDto, Employee>().ReverseMap();
        CreateMap<EmployeeForUpdateValidatorDto, EmployeeForUpdateDto>().ReverseMap();
        CreateMap<WarehouseEmployeeDataDto, Employee>().ReverseMap();
    }
}
