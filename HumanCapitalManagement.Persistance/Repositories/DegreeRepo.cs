using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.DegreeDTOs;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.Persistance.Repositories;
public class DegreeRepo : IDegreeRepo
{
    private readonly ApplicationDbContext _context;
    public DegreeRepo(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Degree>> GetDegrees()
    {
        return await _context.Degrees.ToListAsync();
    }

    public async Task<Degree?> GetDegree(int degreeId)
    {
        return await _context.Degrees
            .FirstOrDefaultAsync(a => a.Id == degreeId);
    }
}
