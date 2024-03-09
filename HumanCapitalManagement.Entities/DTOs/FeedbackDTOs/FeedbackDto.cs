namespace HumanCapitalManagement.Entities.DTOs.FeedbackDTOs
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTimeOffset InsertedDate { get; set; }
        public string InsertedUsername { get; set; } = string.Empty;
        public bool IsSent { get; set; }
        public int FromEmployeeId { get; set; }
        public int ToEmployeeId { get; set; }
    }
}
