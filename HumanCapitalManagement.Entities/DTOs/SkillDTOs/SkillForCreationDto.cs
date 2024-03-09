namespace HumanCapitalManagement.Entities.DTOs.SkillDTOs;

public class SkillForCreationDto
{
    public string Description { get; set; } = string.Empty;
    public ICollection<int>? CollectionOfSkills { get; set; }
}
