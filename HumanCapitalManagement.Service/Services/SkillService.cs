using AutoMapper;
using FluentValidation;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.SkillDTOs;
using HumanCapitalManagement.Persistance.Repositories;

namespace HumanCapitalManagement.Service.Services;
public class SkillService : ISkillService
{
    private readonly IMapper _mapper;
    private readonly ISkillRepo _skillRepo;
    private readonly IEntitiesRepo _entitiesRepo;
    private readonly IValidator<SkillForCreationValidatorDto> _createSkillValidator;
    private readonly IValidator<SkillForUpdateValidatorDto> _updateSkillValidator;
    private readonly IValidator<SkillExistanceValidatorDto> _skillExistanceValidator;

    public SkillService(
        IMapper mapper, 
        ISkillRepo skillRepo,
        IEntitiesRepo entitiesRepo,
        IValidator<SkillForCreationValidatorDto> createSkillValidator,
        IValidator<SkillExistanceValidatorDto> skillExistanceValidator,
        IValidator<SkillForUpdateValidatorDto> updateSkillValidator)
	{
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _skillRepo = skillRepo ?? throw new ArgumentNullException(nameof(skillRepo));
        _entitiesRepo = entitiesRepo ?? throw new ArgumentNullException(nameof(entitiesRepo));
        _createSkillValidator = createSkillValidator ?? throw new ArgumentNullException(nameof(createSkillValidator));
        _skillExistanceValidator = skillExistanceValidator ?? throw new ArgumentNullException(nameof(skillExistanceValidator));
        _updateSkillValidator = updateSkillValidator ?? throw new ArgumentNullException(nameof(updateSkillValidator));
    }

    public async Task<ICollection<SkillDto>> GetSkills()
    {
        var skills = await _skillRepo.GetSkills();
        var skillsToReturn = _mapper.Map<ICollection<SkillDto>>(skills);

        return skillsToReturn;
    }

    public async Task<SkillDto?> GetSkill(int skillId)
    {
        Skill? skill = await _skillRepo.GetSkill(skillId);
        SkillDto skillDto = _mapper.Map<SkillDto>(skill);

        return skillDto;
    }

    public async Task<SkillDto> CreateSkill(SkillForCreationDto skillForCreationDto)
    {
        await _createSkillValidator.ValidateAndThrowAsync(
            new SkillForCreationValidatorDto 
            { 
                Description = skillForCreationDto.Description 
            });

        Skill skillModel = _mapper.Map<Skill>(skillForCreationDto);

        await _skillRepo.AddSkill(skillModel);
        await _entitiesRepo.SaveChanges();

        SkillDto skillToReturn = _mapper.Map<SkillDto>(skillModel);

        return skillToReturn;
    }

    public async Task UpdateSkill(int skillId, SkillForUpdateDto skillForUpdateDto)
    {
        Skill? skill = await _skillRepo.GetSkill(skillId);
        await _skillExistanceValidator.ValidateAndThrowAsync(
            new SkillExistanceValidatorDto 
            { 
                Skill = skill 
            });

        await _updateSkillValidator.ValidateAndThrowAsync(
            new SkillForUpdateValidatorDto
            {
                Description = skillForUpdateDto.Description
            });

        _mapper.Map(skillForUpdateDto, skill);

        _skillRepo.UpdateSkill(skill!);

        await _entitiesRepo.SaveChanges();
    }

    public async Task DeleteSkill(int skillId)
    {
        Skill? skillToDelete = await _skillRepo.GetSkill(skillId);

        await _skillExistanceValidator.ValidateAndThrowAsync(
            new SkillExistanceValidatorDto 
            { 
                Skill = skillToDelete
            });

        _skillRepo.RemoveSkill(skillToDelete!);

        await _entitiesRepo.SaveChanges();
    }

}
