using HumanCapitalManagement.Entities.DTOs.DegreeDTOs;

namespace HumanCapitalManagement.Service.Services;
public interface IDegreeService
{
    Task<ICollection<DegreeDto>> GetDegrees();
    Task<DegreeDto?> GetDegree(int degreeId);
}
