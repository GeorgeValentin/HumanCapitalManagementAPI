using AutoMapper;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.ContractDTOs;

namespace HumanCapitalManagement.Entities.Profiles;
public class ContractProfile : Profile
{
	public ContractProfile()
	{
        CreateMap<Contract, ContractDto>().ReverseMap();
        CreateMap<Contract, ContractForCreationDto>().ReverseMap();
        CreateMap<Contract, ContractForUpdateDto>().ReverseMap();
    }
}
