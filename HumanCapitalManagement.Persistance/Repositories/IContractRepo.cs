using HumanCapitalManagement.Domain.Models;

namespace HumanCapitalManagement.Persistance.Repositories;
public interface IContractRepo
{
    Task AddContract(Contract contractModel);
    Task<Contract?> GetContract(int contractId);
    Task<ICollection<Contract>> GetContracts();
    Task<ICollection<Contract>> GetEmployeeContracts(int employeeId);
    void UpdateContract(Contract contract);
}
