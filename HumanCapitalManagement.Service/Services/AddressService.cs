using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Persistance.Repositories;

namespace HumanCapitalManagement.Service.Services;
public class AddressService : IAddressService
{
    private readonly IAddressRepo _addressRepo;

    public AddressService(IAddressRepo addressRepo)
    {
        _addressRepo = addressRepo ?? throw new ArgumentNullException(nameof(addressRepo));
    }

    public async Task AddAddress(Address address)
    {
        await _addressRepo.AddAddress(address!);
    }

    public Task<Address?> GetAddress(int addressId)
    {
        return _addressRepo.GetAddress(addressId);
    }

    public void UpdateAddress(Address? address)
    {
        _addressRepo.UpdateAddress(address);
    }
}
