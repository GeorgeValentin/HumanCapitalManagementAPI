using AutoMapper;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.DegreeDTOs;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;
using HumanCapitalManagement.Entities.DTOs.FacultyDTOs;
using HumanCapitalManagement.Entities.DTOs.InstitutionDTOs;
using HumanCapitalManagement.Entities.DTOs.StudyProgramDTOs;

namespace HumanCapitalManagement.Entities.Profiles
{
    public class InstitutionProfile : Profile
    {
        public InstitutionProfile()
        {
            CreateMap<Institution, InstitutionDto>().ReverseMap();
            CreateMap<Faculty, FacultyDto>().ReverseMap();
            CreateMap<Institution, InstitutionForCreationDto>().ReverseMap();
            CreateMap<Faculty, FacultyForCreationDto>().ReverseMap();
            CreateMap<StudyProgram, StudyProgramDto>().ReverseMap();
            CreateMap<StudyProgram, StudyProgramForCreationDto>().ReverseMap();
            CreateMap<CreateInstitutionValidatorDto, InstitutionForCreationDto>().ReverseMap();
            CreateMap<InstitutionDto, InstitutionForCreationDto>().ReverseMap();
            CreateMap<FacultyDto, FacultyForCreationDto>().ReverseMap();
            CreateMap<StudyProgramDto, StudyProgramForCreationDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<ICollection<Degree>, ICollection<DegreeDto>>().ReverseMap();
            CreateMap<Degree, DegreeDto>().ReverseMap();
        }
    }
}
