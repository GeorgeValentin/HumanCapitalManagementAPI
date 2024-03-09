using HumanCapitalManagement.Domain.Models;

namespace HumanCapitalManagement.Entities.DTOs.ContractDTOs;
public class ContractCreateValidatorDto
{
    public int EmployeeId { get; set; }
    public ContractForCreationDto ContractForCreationDto { get; set; } = new ContractForCreationDto();
}
