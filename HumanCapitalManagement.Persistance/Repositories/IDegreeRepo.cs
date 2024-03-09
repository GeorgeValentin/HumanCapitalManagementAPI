using HumanCapitalManagement.Domain.Models;

namespace HumanCapitalManagement.Persistance.Repositories;
public interface IDegreeRepo
{
    Task<ICollection<Degree>> GetDegrees();
    Task<Degree?> GetDegree(int degreeId);

}
