using HumanCapitalManagement.Domain.Models;

namespace HumanCapitalManagement.Service.Services;
public interface IAddressService
{
    Task AddAddress(Address address);
    Task<Address?> GetAddress(int addressId);
    void UpdateAddress(Address address);
}
