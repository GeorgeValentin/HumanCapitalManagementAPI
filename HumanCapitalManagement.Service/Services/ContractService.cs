using AutoMapper;
using FluentValidation;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.ContractDTOs;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;
using HumanCapitalManagement.Entities.Exceptions;
using HumanCapitalManagement.Persistance.Repositories;

namespace HumanCapitalManagement.Service.Services;
public class ContractService : IContractService
{
    private readonly IContractRepo _contractRepo;
    private readonly IMapper _mapper;
    private readonly IEntitiesRepo _entitiesRepo;
    private readonly IEmployeeRepo _employeeRepo;
    private readonly IValidator<ContractCreateValidatorDto> _createContractValidator;
    private readonly IValidator<ContractUpdateValidatorDto> _updateContractValidator;
    private readonly IValidator<EmployeeExistanceValidatorDto> _employeeExistanceValidator;
    private readonly IValidator<ContractExistanceValidatorDto> _contractExistanceValidator;

    public ContractService(
        IContractRepo contractRepo,
        IMapper mapper,
        IEntitiesRepo entitiesRepo,
        IEmployeeRepo employeeRepo,
        IValidator<ContractCreateValidatorDto> createContractValidator,
        IValidator<ContractUpdateValidatorDto> updateContractValidator,
        IValidator<EmployeeExistanceValidatorDto> employeeExistanceValidator,
        IValidator<ContractExistanceValidatorDto> contractExistanceValidator)
    {
        _contractRepo = contractRepo ?? throw new ArgumentNullException(nameof(contractRepo));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _entitiesRepo = entitiesRepo ?? throw new ArgumentNullException(nameof(entitiesRepo));
        _employeeRepo = employeeRepo ?? throw new ArgumentNullException(nameof(employeeRepo));
        _createContractValidator = createContractValidator ?? throw new ArgumentNullException(nameof(createContractValidator));
        _updateContractValidator = updateContractValidator ?? throw new ArgumentNullException(nameof(updateContractValidator));
        _employeeExistanceValidator = employeeExistanceValidator ?? throw new ArgumentNullException(nameof(employeeExistanceValidator));
        _contractExistanceValidator = contractExistanceValidator ?? throw new ArgumentNullException(nameof(contractExistanceValidator));
    }

    public async Task<ContractDto?> GetContract(int contractId)
    {
        Contract? contract = await _contractRepo.GetContract(contractId);
        await _contractExistanceValidator.ValidateAndThrowAsync(
            new ContractExistanceValidatorDto { Contract = contract });

        ContractDto contractDto = _mapper.Map<ContractDto>(contract);

        return contractDto;
    }

    public async Task<ICollection<ContractDto>> GetEmployeeContracts(int employeeId)
    {
        Employee? contractEmployee = await _employeeRepo.GetEmployee(employeeId);
        await _employeeExistanceValidator.ValidateAndThrowAsync(
            new EmployeeExistanceValidatorDto { Employee = contractEmployee, EmployeeId = employeeId });

        ICollection<Contract> employeeContracts = await _contractRepo.GetEmployeeContracts(employeeId);
        ICollection<ContractDto> employeeContractsToReturn = employeeContracts
            .Select(elem => _mapper.Map<ContractDto>(elem))
            .ToList();

        return employeeContractsToReturn;
    }

    public async Task<ContractDto> AddContract(int employeeId, ContractForCreationDto contractForCreationDto)
    {
        Employee? contractEmployee = await _employeeRepo.GetEmployee(employeeId);
        await _employeeExistanceValidator.ValidateAndThrowAsync(
             new EmployeeExistanceValidatorDto { Employee = contractEmployee, EmployeeId = employeeId });

        await _createContractValidator.ValidateAndThrowAsync(
            new ContractCreateValidatorDto
            {
                ContractForCreationDto = contractForCreationDto
            });

        ICollection<Contract> empContracts = await _contractRepo.GetEmployeeContracts(employeeId);
        Contract? latestDatedContractVariant = null;

        ICollection<Contract> sortedEmpContracts = empContracts
            .OrderByDescending(a => a.EndDate)
            .ToList();

        if (sortedEmpContracts.Count > 0)
        {
            latestDatedContractVariant = sortedEmpContracts.Last();

            if (DateTimeOffset.Compare(contractForCreationDto.StartDate.Date, latestDatedContractVariant.StartDate.Date) <= 0)
                throw new InvalidDateException("Please enter the new variant of the contract having a later date than the current one!");
            
            latestDatedContractVariant.EndDate = contractForCreationDto.StartDate;

            _contractRepo.UpdateContract(latestDatedContractVariant);
        }

        Contract contractModel = _mapper.Map<Contract>(contractForCreationDto);

        contractModel.InsertedDate = DateTimeOffset.UtcNow.Date;
        contractModel.InsertedUsername = $"{contractEmployee!.FirstName} {contractEmployee.LastName}";
    
        await _contractRepo.AddContract(contractModel);
    
        contractModel.EmployeeId = employeeId;
        contractModel.JobTitleId = contractForCreationDto.JobTitleId;
    
        await _entitiesRepo.SaveChanges();
        
        var contractToReturn = _mapper.Map<ContractDto>(contractModel);

        return contractToReturn;
    }

    public async Task UpdateContract(int employeeId, int contractId, ContractForUpdateDto contractForUpdateDto)
    {
        Employee? attachedEmployee = await _employeeRepo.GetEmployee(employeeId);
        await _employeeExistanceValidator.ValidateAndThrowAsync(
             new EmployeeExistanceValidatorDto { Employee = attachedEmployee, EmployeeId = employeeId });

        Contract? contractToUpdate = await _contractRepo.GetContract(contractId);
        await _contractExistanceValidator.ValidateAndThrowAsync(
                    new ContractExistanceValidatorDto { Contract = contractToUpdate });

        await _updateContractValidator.ValidateAndThrowAsync(
            new ContractUpdateValidatorDto
            {
                JobTitleId = contractForUpdateDto.JobTitleId
            });
        contractToUpdate!.JobTitleId = contractForUpdateDto.JobTitleId;
        _contractRepo.UpdateContract(contractToUpdate);

        if (contractForUpdateDto.StartDate != null)
        {
            ICollection<Contract> empContracts = await _contractRepo.GetEmployeeContracts(employeeId);
            ICollection<Contract> sortedEmpContracts = empContracts
                .OrderBy(a => a.StartDate)
                .ToList();

            Contract latestContract = sortedEmpContracts.Last();
            if (DateTimeOffset.Compare(contractForUpdateDto.StartDate!.Value.Date, latestContract.StartDate.Date) > 0)
                throw new InvalidDateException("Please enter a date at an earlier point in time than the most recent contract variant!");

            if (DateTimeOffset.Compare((DateTimeOffset)contractForUpdateDto.StartDate!.Value.Date, DateTimeOffset.UtcNow.Date) < 0)
                throw new InvalidDateException("Please enter a date at a later point in time than the current date!");
            
            contractToUpdate.StartDate = contractForUpdateDto.StartDate.Value.Date;
        }

        contractToUpdate.UpdatedDate = DateTimeOffset.UtcNow;
        contractToUpdate.UpdatedUsername = $"{attachedEmployee!.FirstName} {attachedEmployee.LastName}";

        await _entitiesRepo.SaveChanges();
    }

    public async Task<ICollection<ContractDto>?> GetContracts()
    {
        ICollection<Contract>? contracts = await _contractRepo.GetContracts();
        var contractsToReturn = contracts
            .Select(elem => _mapper.Map<ContractDto>(elem))
            .ToList();

        return contractsToReturn;
    }
}
