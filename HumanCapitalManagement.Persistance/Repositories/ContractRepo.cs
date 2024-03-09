using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Utilities.Logging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace HumanCapitalManagement.Persistance.Repositories;
public class ContractRepo : IContractRepo
{
    private readonly ApplicationDbContext _context;

    public ContractRepo(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Contract?> GetContract(int contractId)
    {
        Contract? contract = await _context.Contracts
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == contractId);

        Log.Information("[{class}.{method}] has been called, returning the contract {contract} from the context.", 
            this.GetType().Name, LoggingHelper.GetActualAsyncMethodName(), JsonConvert.SerializeObject(contract));

        return contract;
    }

    public async Task<ICollection<Contract>> GetEmployeeContracts(int employeeId)
    {
        ICollection<Contract> employeeContracts = await _context.Contracts
            .AsNoTracking()
            .Where(c => c.EmployeeId == employeeId)
            .ToListAsync();

        Log.Information("[{class}.{method}] has been called, returning {contractsCounter} contracts from the context.", 
            this.GetType().Name, LoggingHelper.GetActualAsyncMethodName(), employeeContracts.Count);

        return employeeContracts;
    }

    public async Task AddContract(Contract contractModel)
    {
        Log.Information("[{class}.{method}] has been called, adding " +
            "a contract to the context.", this.GetType().Name, LoggingHelper.GetActualAsyncMethodName());

        await _context.Contracts.AddAsync(contractModel);
    }

    public void UpdateContract(Contract contract)
    {
        Log.Information("[{class}.{method}] has been called, " +
            "updating a contract in the context.", this.GetType().Name, LoggingHelper.GetActualAsyncMethodName());

        _context.Contracts.Update(contract);
    }

    public async Task<ICollection<Contract>> GetContracts()
    {
        return await _context.Contracts.ToListAsync();
    }
}
