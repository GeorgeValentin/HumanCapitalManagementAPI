using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.ContractDTOs;

namespace HumanCapitalManagement.Service.Services;
public interface IContractService
{
    Task<ContractDto?> GetContract(int contractId);
    Task<ContractDto> AddContract(int employeeId, ContractForCreationDto contractForCreationDto);
    Task UpdateContract(int employeeId, int contractId, ContractForUpdateDto contractForUpdateDto);
    Task<ICollection<ContractDto>> GetEmployeeContracts(int employeeId);
    Task<ICollection<ContractDto>?> GetContracts();
}
