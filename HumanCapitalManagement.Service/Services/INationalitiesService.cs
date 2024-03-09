using HumanCapitalManagement.Entities.DTOs.NationalityDTOs;

namespace HumanCapitalManagement.Service.Services;
public interface INationalitiesService
{
    Task<ICollection<NationalityDto>> GetNationalities();
    Task<NationalityDto> GetNationality(int nationalityId);
}
