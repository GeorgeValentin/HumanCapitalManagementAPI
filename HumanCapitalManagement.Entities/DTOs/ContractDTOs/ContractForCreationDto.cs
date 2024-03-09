namespace HumanCapitalManagement.Entities.DTOs.ContractDTOs;
public class ContractForCreationDto
{
    public DateTimeOffset StartDate { get; set; }
    public double Salary { get; set; }
    public int JobTitleId { get; set; }
}
