namespace HumanCapitalManagement.Entities.DTOs.FeedbackDTOs
{
    public class FeedbackForCreationDto
    {
        public string Content { get; set; } = string.Empty;
        public bool IsSent { get; set; }
        public int FromEmployeeId { get; set; }
        public int ToEmployeeId { get; set; }
    }
}
