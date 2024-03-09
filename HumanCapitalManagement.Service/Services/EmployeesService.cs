using AutoMapper;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Persistance.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;
using FluentValidation;
using HumanCapitalManagement.Entities.DTOs.AddressDTOs;
using HumanCapitalManagement.Entities.DTOs.ContractDTOs;
using Microsoft.Extensions.Configuration;
using HumanCapitalManagement.Service.Pagination;
using HumanCapitalManagement.Entities.DTOs.PaginationDTOs;

namespace HumanCapitalManagement.Service.Services;

public class EmployeesService : IEmployeeService
{
    private readonly IEntitiesRepo _entitiesRepo;
    private readonly IEmployeeRepo _employeeRepo;
    private readonly IContractRepo _contractRepo;
    private readonly IAddressService _addressService;
    private readonly IMapper _mapper;
    private readonly IAzureServiceBus _azureServiceBus;
    private readonly IConfiguration _configuration; 
    private readonly IValidator<AddressForCreationValidatorDto> _createAddressValidator;
    private readonly IValidator<EmployeeForCreationValidatorDto> _createEmployeeValidator;
    private readonly IValidator<EmployeeForUpdateValidatorDto> _updateEmployeeValidator;
    private readonly IValidator<ContractExistanceValidatorDto> _contractExistanceValidator;
    private readonly IValidator<EmployeeExistanceValidatorDto> _employeeExistanceValidator;

    public EmployeesService(
        IEmployeeRepo employeeRepo,
        IContractRepo contractRepo,
        IAddressService addressService,
        IMapper mapper,
        IEntitiesRepo entitiesRepo,
        IConfiguration configuration,
        IAzureServiceBus azureServiceBus,
        IValidator<AddressForCreationValidatorDto> createAddressValidator,
        IValidator<EmployeeForCreationValidatorDto> createEmployeeValidator,
        IValidator<EmployeeForUpdateValidatorDto> updateEmployeeValidator,
        IValidator<ContractExistanceValidatorDto> contractExistanceValidator,
        IValidator<EmployeeExistanceValidatorDto> employeeExistanceValidator)
    {
        _employeeRepo = employeeRepo ?? throw new ArgumentNullException(nameof(employeeRepo));
        _contractRepo = contractRepo ?? throw new ArgumentNullException(nameof(contractRepo));
        _addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
        _entitiesRepo = entitiesRepo ?? throw new ArgumentNullException(nameof(entitiesRepo));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _createAddressValidator = createAddressValidator ?? throw new ArgumentNullException(nameof(createAddressValidator));
        _createEmployeeValidator = createEmployeeValidator ?? throw new ArgumentNullException(nameof(createEmployeeValidator));
        _updateEmployeeValidator = updateEmployeeValidator ?? throw new ArgumentNullException(nameof(updateEmployeeValidator));
        _contractExistanceValidator = contractExistanceValidator ?? throw new ArgumentNullException(nameof(contractExistanceValidator));
        _employeeExistanceValidator = employeeExistanceValidator ?? throw new ArgumentNullException(nameof(employeeExistanceValidator));
        _azureServiceBus = azureServiceBus ?? throw new ArgumentNullException(nameof(azureServiceBus));
    }

    public async Task<List<EmployeeDto>> GetEmployees(PaginationParameters? productParameters = null)
    {
        ICollection<Employee> employees = await _employeeRepo.GetEmployees();
        ICollection<EmployeeDto> employeesDto = _mapper.Map<ICollection<EmployeeDto>>(employees);

        if(productParameters == null || productParameters.PageNumber == 0 || productParameters.PageSize == 0)
        {
            return PagedList<EmployeeDto>
                .ToPagedList(employeesDto.ToList(), pageNumber: 0, pageSize: 0);
        } else
        {
            IEnumerable<EmployeeDto> listWithPagination = employeesDto.ToList();

            return PagedList<EmployeeDto>
                .ToPagedList(listWithPagination, productParameters.PageNumber, productParameters.PageSize);
        }

    }

    public async Task<EmployeeDto> GetEmployee(int employeeId)
    {
        Employee? employee = await _employeeRepo.GetEmployee(employeeId);
        await _employeeExistanceValidator.ValidateAndThrowAsync(
            new EmployeeExistanceValidatorDto { Employee = employee, EmployeeId = employeeId });

        EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);

        return employeeDto;
    }

    public async Task<EmployeeDto> AddEmployee(EmployeeForCreationDto employeeForCreationDto)
    {
        Employee employeeModel = _mapper.Map<Employee>(employeeForCreationDto);

        employeeModel.InsertedUsername = $"{employeeForCreationDto.FirstName} {employeeForCreationDto.LastName}";
        employeeModel.InsertedDate = DateTimeOffset.UtcNow;

        await _createEmployeeValidator.ValidateAndThrowAsync(_mapper.Map<EmployeeForCreationValidatorDto>(employeeModel));
        await _employeeRepo.AddEmployee(employeeModel);

        await _createAddressValidator.ValidateAndThrowAsync(_mapper.Map<AddressForCreationValidatorDto>(employeeForCreationDto.Address));
        await _addressService.AddAddress(employeeForCreationDto.Address);

        employeeModel.NationalityId = employeeForCreationDto.NationalityId;
        employeeModel.DepartmentId = employeeForCreationDto.DepartmentId;

        await _entitiesRepo.SaveChanges();

        Employee? addedEmployee = await _employeeRepo.GetEmployee(employeeModel.Id);
        EmployeeDto employeeToReturn = _mapper.Map<EmployeeDto>(addedEmployee);

        return employeeToReturn;
    }

    public async Task UpdateEmployee(int employeeId, EmployeeForUpdateDto employeeForUpdateDto)
    {
        Employee? employee = await _employeeRepo.GetEmployee(employeeId);
        await _employeeExistanceValidator.ValidateAndThrowAsync(
            new EmployeeExistanceValidatorDto { Employee = employee, EmployeeId = employeeId });

        await _updateEmployeeValidator.ValidateAndThrowAsync(_mapper.Map<EmployeeForUpdateValidatorDto>(employeeForUpdateDto));
        await _createAddressValidator.ValidateAndThrowAsync(_mapper.Map<AddressForCreationValidatorDto>(employeeForUpdateDto.Address));

        employeeForUpdateDto!.Address!.Id = employee!.AddressId;
        _addressService.UpdateAddress(employeeForUpdateDto.Address);
        await _entitiesRepo.SaveChanges();

        employee!.UpdateUsername = $"{employeeForUpdateDto.FirstName} {employeeForUpdateDto.LastName}";
        employee.UpdateDate = DateTimeOffset.UtcNow;
        
        _mapper.Map(employeeForUpdateDto, employee);
        employee.Department = null;
        employee.Nationality = null;

        _employeeRepo.UpdateEmployee(employee);
        await _entitiesRepo.SaveChanges();
    }

    public async Task UpdateEmployeeProduction(int employeeId, EmployeeForUpdateDto employeeForUpdateDto)
    {
        Employee? employee = await _employeeRepo.GetEmployee(employeeId);
        await _employeeExistanceValidator.ValidateAndThrowAsync(
            new EmployeeExistanceValidatorDto { Employee = employee, EmployeeId = employeeId });

        await _updateEmployeeValidator.ValidateAndThrowAsync(_mapper.Map<EmployeeForUpdateValidatorDto>(employeeForUpdateDto));
        await _createAddressValidator.ValidateAndThrowAsync(_mapper.Map<AddressForCreationValidatorDto>(employeeForUpdateDto.Address));

        employee!.UpdateUsername = $"{employeeForUpdateDto.FirstName} {employeeForUpdateDto.LastName}";
        employee.UpdateDate = DateTimeOffset.UtcNow;

        _mapper.Map(employeeForUpdateDto, employee);
        employee.Department = null;
        employee.Nationality = null;

        _employeeRepo.UpdateEmployee(employee);
        await _entitiesRepo.SaveChanges();

        WarehouseEmployeeDataDto warehouseEmployee = _mapper.Map<WarehouseEmployeeDataDto>(employee);
        await _azureServiceBus.SendMessageAsync(warehouseEmployee, _configuration["AzureServiceBus:EmployeeQueueName"]);
    }

    public async Task DeleteEmployee(
        int employeeId,
        JsonPatchDocument<EmployeeForCreationDto> patchDocument)
    {
        Employee? employeeToDelete = await _employeeRepo.GetEmployee(employeeId);
        await _employeeExistanceValidator.ValidateAndThrowAsync(
            new EmployeeExistanceValidatorDto { Employee = employeeToDelete, EmployeeId = employeeId });

        _employeeRepo.DeleteEmployee(employeeToDelete!, patchDocument);
        await _entitiesRepo.SaveChanges();

        ICollection<Contract> employeeContractsToUpdate = await _contractRepo.GetEmployeeContracts(employeeId);
        if(employeeContractsToUpdate != null && employeeContractsToUpdate.Count > 0)
        {
            var employeePreLeavingDays = 20;
            var latestContractOfEmployee = employeeContractsToUpdate.LastOrDefault();
            await _contractExistanceValidator.ValidateAndThrowAsync(new ContractExistanceValidatorDto { Contract = latestContractOfEmployee });

            latestContractOfEmployee!.EndDate = latestContractOfEmployee.StartDate.AddDays(employeePreLeavingDays);
            _contractRepo.UpdateContract(latestContractOfEmployee);
            await _entitiesRepo.SaveChanges();
        }

    }

}
