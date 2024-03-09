using AutoMapper;
using FluentValidation;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;
using HumanCapitalManagement.Entities.DTOs.SkillDTOs;
using HumanCapitalManagement.Persistance.Repositories;

namespace HumanCapitalManagement.Service.Services;
public class EmployeeSkillService : IEmployeeSkillService
{
    private readonly ISkillRepo _skillRepo;
    private readonly IEmployeeSkillRepo _employeeSkillRepo;
    private readonly IEntitiesRepo _entitiesRepo;
    private readonly IMapper _mapper;
    private readonly IEmployeeRepo _employeeRepo;
    private readonly IValidator<EmployeeSkillsToDeleteValidatorDto> _deleteEmployeeSkillsValidator;
    private readonly IValidator<EmployeeSkillToDeleteValidatorDto> _deleteEmployeeSkillValidator;
    private readonly IValidator<EmployeeSkillForCreationValidatorDto> _createEmployeeSkilValidator;
    private readonly IValidator<EmployeeExistanceValidatorDto> _employeeExistanceValidator;


    public EmployeeSkillService(
        ISkillRepo skillRepo,
        IMapper mapper,
        IEntitiesRepo entitiesRepo,
        IEmployeeSkillRepo employeeSkillRepo,
        IEmployeeRepo employeeRepo,
        IValidator<EmployeeSkillsToDeleteValidatorDto> deleteEmployeeSkillsValidator,
        IValidator<EmployeeSkillToDeleteValidatorDto> deleteEmployeeSkillValidator,
        IValidator<EmployeeSkillForCreationValidatorDto> createEmployeeSkilValidator,
        IValidator<EmployeeExistanceValidatorDto> employeeExistanceValidator)
    {
        _skillRepo = skillRepo ?? throw new ArgumentNullException(nameof(skillRepo));
        _employeeSkillRepo = employeeSkillRepo ?? throw new ArgumentNullException(nameof(employeeSkillRepo));
        _entitiesRepo = entitiesRepo ?? throw new ArgumentNullException(nameof(entitiesRepo));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _employeeRepo = employeeRepo ?? throw new ArgumentNullException(nameof(employeeRepo));
        _deleteEmployeeSkillsValidator = deleteEmployeeSkillsValidator ?? throw new ArgumentNullException(nameof(deleteEmployeeSkillsValidator));
        _deleteEmployeeSkillValidator = deleteEmployeeSkillValidator ?? throw new ArgumentNullException(nameof(deleteEmployeeSkillValidator));
        _createEmployeeSkilValidator = createEmployeeSkilValidator ?? throw new ArgumentNullException(nameof(createEmployeeSkilValidator));
        _employeeExistanceValidator = employeeExistanceValidator ?? throw new ArgumentNullException(nameof(employeeExistanceValidator));
    }

    public async Task<ICollection<SkillDto>> GetSkillsOfEmployee(int employeeId)
    {
        Employee? employee = await _employeeRepo.GetEmployee(employeeId);
        await _employeeExistanceValidator.ValidateAndThrowAsync(
            new EmployeeExistanceValidatorDto { Employee = employee, EmployeeId = employeeId });

        ICollection<Skill> skills = await _skillRepo.GetSkillsOfEmployee(employeeId);
        ICollection<SkillDto> skillsToReturn = _mapper.Map<ICollection<SkillDto>>(skills);

        return skillsToReturn;
    }

    public async Task<SkillDto?> GetSkillOfEmployee(int employeeId, int skillId)
    {
        var employee = await _employeeRepo.GetEmployee(employeeId);
        await _employeeExistanceValidator.ValidateAndThrowAsync(
            new EmployeeExistanceValidatorDto { Employee = employee, EmployeeId = employeeId });
        
        Skill? skill = await _skillRepo.GetSkillOfEmployee(employeeId, skillId);
        SkillDto? skillDto = _mapper.Map<SkillDto>(skill);

        return skillDto;
    }

    public async Task<ICollection<SkillDto>> AddSkillsToEmployee(int employeeId, SkillForCreationDto skillsForCreationDto)
    {
        var employee = await _employeeRepo.GetEmployee(employeeId);
        await _employeeExistanceValidator.ValidateAndThrowAsync(
            new EmployeeExistanceValidatorDto { Employee = employee, EmployeeId = employeeId });

        if (skillsForCreationDto.CollectionOfSkills != null && skillsForCreationDto.CollectionOfSkills.Count > 0)
        {
            ICollection<EmployeeSkill> employeeSkillCollection =
                skillsForCreationDto.CollectionOfSkills
                    .Select((skillId) => new EmployeeSkill { EmployeeId = employeeId, SkillID = skillId })
                    .ToList();
            await _createEmployeeSkilValidator.ValidateAndThrowAsync(
                new EmployeeSkillForCreationValidatorDto { EmployeeSkills = employeeSkillCollection });

            await _employeeSkillRepo.AddEmployeeSkills(employeeSkillCollection!);
            await _entitiesRepo.SaveChanges();
        }

        var skills = await _skillRepo.GetSkillsOfEmployee(employeeId);
        var skillsToReturn = _mapper.Map<ICollection<SkillDto>>(skills);

        return skillsToReturn;
    }

    public async Task DeleteAllSkillsOfEmployeees(int employeeId)
    {
        Employee? employee = await _employeeRepo.GetEmployee(employeeId);
        await _employeeExistanceValidator.ValidateAndThrowAsync(
            new EmployeeExistanceValidatorDto { Employee = employee, EmployeeId = employeeId });

        var employeeSkillsRelationsToDelete = await _employeeSkillRepo.GetEmployeeSkills(employeeId);
        await _deleteEmployeeSkillsValidator.ValidateAndThrowAsync(
            new EmployeeSkillsToDeleteValidatorDto { EmployeeSkills = employeeSkillsRelationsToDelete });

        _employeeSkillRepo.DeleteEmployeeSkills(employeeSkillsRelationsToDelete);
        await _entitiesRepo.SaveChanges();
    }

    public async Task DeleteOneSkillOfEmployee(int employeeId, int skillId)
    {
        Employee? employee = await _employeeRepo.GetEmployee(employeeId);
        await _employeeExistanceValidator.ValidateAndThrowAsync(
            new EmployeeExistanceValidatorDto { Employee = employee, EmployeeId = employeeId });

        var employeeSkillRelationToDelete = await _employeeSkillRepo.GetEmployeeSkill(employeeId, skillId);
        await _deleteEmployeeSkillValidator.ValidateAndThrowAsync(
                new EmployeeSkillToDeleteValidatorDto { EmployeeSkill = employeeSkillRelationToDelete });

        _employeeSkillRepo.DeleteEmployeeSkill(employeeSkillRelationToDelete!);
        await _entitiesRepo.SaveChanges();
    }

}
