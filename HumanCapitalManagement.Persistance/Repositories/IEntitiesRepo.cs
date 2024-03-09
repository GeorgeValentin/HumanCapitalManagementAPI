namespace HumanCapitalManagement.Persistance.Repositories;
public interface IEntitiesRepo
{
    Task<bool> SaveChanges();
}
