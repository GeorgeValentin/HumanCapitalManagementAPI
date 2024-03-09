using AutoMapper;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.NationalityDTOs;
using HumanCapitalManagement.Persistance.Repositories;

namespace HumanCapitalManagement.Service.Services;
public class NationalitiesService : INationalitiesService
{
    private readonly INationalityRepo _nationalitiesRepo;
    private readonly IMapper _mapper;

    public NationalitiesService(
        INationalityRepo nationalitiesRepo,
        IMapper mapper)
    {
        _nationalitiesRepo = nationalitiesRepo ?? throw new ArgumentNullException(nameof(nationalitiesRepo));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<ICollection<NationalityDto>> GetNationalities()
    {
        ICollection<Nationality> nationalities = await _nationalitiesRepo.GetNationalities();
        var nationalitiesToReturn = nationalities
            .Select(elem => _mapper.Map<NationalityDto>(elem))
            .ToList();

        return nationalitiesToReturn;
    }

    public async Task<NationalityDto> GetNationality(int nationalityId)
    {
        Nationality? nationality = await _nationalitiesRepo.GetNationality(nationalityId);
        var nationalityToReturn = _mapper.Map<NationalityDto>(nationality);

        return nationalityToReturn;
    }
}
