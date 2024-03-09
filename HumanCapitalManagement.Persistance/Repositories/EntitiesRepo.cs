using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Utilities.Logging;
using Serilog;

namespace HumanCapitalManagement.Persistance.Repositories;
public class EntitiesRepo : IEntitiesRepo
{
    private readonly ApplicationDbContext _context;

    public EntitiesRepo(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<bool> SaveChanges()
    {
        Log.Information("[{className}.{methodName)}] has been called on the context.", 
            this.GetType().Name, LoggingHelper.GetActualAsyncMethodName()); 

        return (await _context.SaveChangesAsync() > 0);
    }
}
