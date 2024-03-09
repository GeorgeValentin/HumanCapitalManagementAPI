using AutoMapper;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.ContractDTOs;
using HumanCapitalManagement.Entities.DTOs.DegreeDTOs;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;
using HumanCapitalManagement.Persistance.Repositories;

namespace HumanCapitalManagement.Service.Services;
public class DegreeService : IDegreeService
{
    private readonly IDegreeRepo _degreeRepo;
    private readonly IMapper _mapper;

    public DegreeService(
        IDegreeRepo degreeRepo, 
        IMapper mapper)
    {
        _degreeRepo = degreeRepo;
        _mapper = mapper;
    }

    public async Task<ICollection<DegreeDto>> GetDegrees()
    {
        ICollection<Degree> degrees = await _degreeRepo.GetDegrees();
        ICollection<DegreeDto> degreesToReturn = degrees
            .Select(elem => _mapper.Map<DegreeDto>(elem))
            .ToList();

        return degreesToReturn;
    }

    public async Task<DegreeDto?> GetDegree(int degreeId)
    {
        Degree? degree = await _degreeRepo.GetDegree(degreeId);

        DegreeDto? degreeDto = _mapper.Map<DegreeDto>(degree);

        return degreeDto;
    }
}
