using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Utilities.Logging;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace HumanCapitalManagement.Persistance.Repositories;
public class AddressRepo : IAddressRepo
{
    private readonly ApplicationDbContext _context;

    public AddressRepo(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Address?> GetAddress(int addressId)
    {
        Log.Information("[{class}.{method}] has been called, getting an address from " +
            "the context.", this.GetType().Name, LoggingHelper.GetActualAsyncMethodName());

        return await _context.Addresses
            .FirstOrDefaultAsync(a => a.Id == addressId);
    }

    public async Task AddAddress(Address address)
    {
        Log.Information("[{class}.{method}] has been called, adding an address to " +
            "the context.", this.GetType().Name, LoggingHelper.GetActualAsyncMethodName());

        await _context.Addresses.AddAsync(address);    
    }

    public void UpdateAddress(Address address)
    {
        Log.Information("[{class}.{method}] has been called, updating an address.", 
            this.GetType().Name, LoggingHelper.GetActualAsyncMethodName());

        _context.Addresses.Update(address);
    }
}
