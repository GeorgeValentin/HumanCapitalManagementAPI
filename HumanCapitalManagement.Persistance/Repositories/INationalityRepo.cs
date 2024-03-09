using HumanCapitalManagement.Domain.Models;

namespace HumanCapitalManagement.Persistance.Repositories;
public interface INationalityRepo
{
    Task<ICollection<Nationality>> GetNationalities();
    Task<Nationality?> GetNationality(int nationalityId);
}
