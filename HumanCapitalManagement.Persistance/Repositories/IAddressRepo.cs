using HumanCapitalManagement.Domain.Models;

namespace HumanCapitalManagement.Persistance.Repositories;
public interface IAddressRepo
{
    Task<Address?> GetAddress(int addressId);
    Task AddAddress(Address address);
    void UpdateAddress(Address address);
}
