using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.Persistance.Repositories;
public class NationalityRepo : INationalityRepo
{
	private readonly ApplicationDbContext _context;

	public NationalityRepo(ApplicationDbContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
    }

	public async Task<ICollection<Nationality>> GetNationalities()
	{
		return await _context.Nationalities.ToListAsync();
	}

	public async Task<Nationality?> GetNationality(int nationalityId)
	{
        return await _context.Nationalities.FirstOrDefaultAsync(a => a.Id == nationalityId);
    }
}
