namespace HumanCapitalManagement.Entities.DTOs.FeedbackDTOs;
public class FeedbackForCreationValidatorDto
{
    public string Content { get; set; } = string.Empty;
    public string IsSent { get; set; } = string.Empty;
    public int FromEmployeeId { get; set; }
    public int ToEmployeeId { get; set; }
}
