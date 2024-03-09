using AutoMapper;
using FluentValidation;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.FacultyDTOs;
using HumanCapitalManagement.Entities.DTOs.InstitutionDTOs;
using HumanCapitalManagement.Entities.DTOs.StudyProgramDTOs;
using HumanCapitalManagement.Persistance.Repositories;

namespace HumanCapitalManagement.Service.Services
{
    public class InstitutionService : IInstitutionService
    {
        private readonly IEntitiesRepo _entitiesRepo;
        private readonly IInstitutionRepo _institutionRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateInstitutionValidatorDto> _createInstitutionValidator;
        private readonly IValidator<CreateFacultyValidatorDto> _createFacultyValidator;
        private readonly IValidator<CreateStudyProgramValidatorDto> _createStudyProgramValidator;
        private readonly IValidator<InstitutionExistanceValidatorDto> _institutionExistanceValidator;
        private readonly IValidator<FacultyExistanceValidatorDto> _facultyExistanceValidator;

        public InstitutionService(
            IEntitiesRepo entitiesRepo, 
            IInstitutionRepo institutionRepo, 
            IMapper mapper,
            IValidator<CreateInstitutionValidatorDto> createInstitutionValidator,
            IValidator<CreateFacultyValidatorDto> createFacultyValidator,
            IValidator<CreateStudyProgramValidatorDto> createStudyProgramValidator,
            IValidator<InstitutionExistanceValidatorDto> institutionExistanceValidator,
            IValidator<FacultyExistanceValidatorDto> facultyExistanceValidator)
        {
            _entitiesRepo = entitiesRepo ?? throw new ArgumentNullException(nameof(entitiesRepo));
            _institutionRepo = institutionRepo ?? throw new ArgumentNullException(nameof(institutionRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _createInstitutionValidator = createInstitutionValidator ?? throw new ArgumentNullException(nameof(createInstitutionValidator));
            _createFacultyValidator = createFacultyValidator ?? throw new ArgumentNullException(nameof(createFacultyValidator));
            _createStudyProgramValidator = createStudyProgramValidator ?? throw new ArgumentNullException(nameof(createStudyProgramValidator));
            _institutionExistanceValidator = institutionExistanceValidator ?? throw new ArgumentNullException(nameof(institutionExistanceValidator));
            _facultyExistanceValidator = facultyExistanceValidator ?? throw new ArgumentNullException(nameof(facultyExistanceValidator));
        }

        public async Task<ICollection<InstitutionDto>> GetInstitutions()
        {
            ICollection<Institution> institutions = await _institutionRepo.GetInstitutions();
            ICollection<InstitutionDto> institutionsToReturn = _mapper.Map<ICollection<InstitutionDto>>(institutions);

            return institutionsToReturn;
        }

        public async Task<InstitutionDto?> GetInstitution(int institutionId)
        {
            Institution? institution = await _institutionRepo.GetInstitution(institutionId);

            InstitutionDto institutionDto = _mapper.Map<InstitutionDto>(institution);

            return institutionDto;
        }

        public async Task<ICollection<FacultyDto>> GetFaculties(int institutionId)
        {
            Institution? institution = await _institutionRepo.GetInstitution(institutionId);
            await _institutionExistanceValidator.ValidateAndThrowAsync(new InstitutionExistanceValidatorDto { Institution = institution });
            
            ICollection<Faculty> faculties = await _institutionRepo.GetFaculties(institution!);
            ICollection<FacultyDto> facultiesToReturn = _mapper.Map<ICollection<FacultyDto>>(faculties);

            return facultiesToReturn;
        }

        public async Task<FacultyDto?> GetFaculty(int institutionId, int facultyId)
        {
            Institution? institution = await _institutionRepo.GetInstitution(institutionId);
            await _institutionExistanceValidator.ValidateAndThrowAsync(new InstitutionExistanceValidatorDto { Institution = institution });

            Faculty? faculty = await _institutionRepo.GetFaculty(facultyId, institution!);
            await _facultyExistanceValidator.ValidateAndThrowAsync(new FacultyExistanceValidatorDto { Faculty = faculty });
    
            FacultyDto facultyDto = _mapper.Map<FacultyDto>(faculty!);

            return facultyDto;
        }

        public async Task<ICollection<StudyProgramDto>> GetStudyPrograms(int institutionId, int facultyId)
        {
            Institution? institution = await _institutionRepo.GetInstitution(institutionId);
            await _institutionExistanceValidator.ValidateAndThrowAsync(new InstitutionExistanceValidatorDto { Institution = institution});

            Faculty? faculty = await _institutionRepo.GetFaculty(facultyId, institution!);
            await _facultyExistanceValidator.ValidateAndThrowAsync(new FacultyExistanceValidatorDto { Faculty = faculty });

            ICollection<StudyProgram> studyPrograms = await _institutionRepo.GetStudyPrograms(institution!, faculty!);
            ICollection<StudyProgramDto> studyProgramsToReturn = _mapper.Map<ICollection<StudyProgramDto>>(studyPrograms);

            return studyProgramsToReturn;
        }

        public async Task<StudyProgramDto?> GetStudyProgram(int institutionId, int facultyId, int studyProgramId)
        {
            Institution? institution = await _institutionRepo.GetInstitution(institutionId);
            await _institutionExistanceValidator.ValidateAndThrowAsync(new InstitutionExistanceValidatorDto { Institution = institution });
            
            Faculty? faculty = await _institutionRepo.GetFaculty(facultyId, institution!);
            await _facultyExistanceValidator.ValidateAndThrowAsync(new FacultyExistanceValidatorDto { Faculty = faculty });

            StudyProgram? studyProgram = await _institutionRepo.GetStudyProgram(studyProgramId, institution!, faculty!);
            StudyProgramDto studyProgramDto = _mapper.Map<StudyProgramDto>(studyProgram);

            return studyProgramDto;
        }

        public async Task<InstitutionDto> AddInstitution(InstitutionForCreationDto institutionForCreationDto)
        {
            await _createInstitutionValidator.ValidateAndThrowAsync(_mapper.Map<CreateInstitutionValidatorDto>(institutionForCreationDto));
            
            Institution? institution = _mapper.Map<Institution>(institutionForCreationDto);

            await _institutionRepo.AddInstitution(institution);
            await _entitiesRepo.SaveChanges();

            InstitutionDto institutionToReturn = _mapper.Map<InstitutionDto>(institution);
            return institutionToReturn;
        }

        public async Task<FacultyDto> AddFaculty(FacultyForCreationDto facultyForCreationDto, int institutionId)
        {
            Institution? institution = await _institutionRepo.GetInstitution(institutionId);
            await _institutionExistanceValidator.ValidateAndThrowAsync(new InstitutionExistanceValidatorDto { Institution = institution });

            await _createFacultyValidator.ValidateAndThrowAsync(
                new CreateFacultyValidatorDto
                {
                    FacultyForCreationDto = facultyForCreationDto,
                });

            Faculty? faculty = _mapper.Map<Faculty>(facultyForCreationDto);

            await _institutionRepo.AddFaculty(faculty);
            faculty.InstitutionId = institutionId;
            await _entitiesRepo.SaveChanges();

            Faculty? addedFaculty = await _institutionRepo.GetFaculty(faculty.Id, institution!);
            FacultyDto? facultyToReturn = _mapper.Map<FacultyDto>(addedFaculty);

            return facultyToReturn;
        }

        public async Task<StudyProgramDto> AddStudyProgram(
            StudyProgramForCreationDto studyProgramForCreationDto,
            int institutionId,
            int facultyId)
        {
            Institution? institution = await _institutionRepo.GetInstitution(institutionId);
            await _institutionExistanceValidator.ValidateAndThrowAsync(new InstitutionExistanceValidatorDto { Institution = institution });

            Faculty? faculty = await _institutionRepo.GetFaculty(facultyId, institution!);
            await _facultyExistanceValidator.ValidateAndThrowAsync(new FacultyExistanceValidatorDto { Faculty = faculty });

            await _createStudyProgramValidator.ValidateAndThrowAsync(
                new CreateStudyProgramValidatorDto
                {
                    StudyProgramForCreationDto = studyProgramForCreationDto
                });

            StudyProgram? studyProgram = _mapper.Map<StudyProgram>(studyProgramForCreationDto);

            await _institutionRepo.AddStudyProgram(studyProgram);

            studyProgram.FacultyId = facultyId;
            studyProgram.DegreeId = studyProgramForCreationDto.DegreeId;
            // studyProgram.Faculty.InstitutionId = institutionId;

            await _entitiesRepo.SaveChanges();

            StudyProgram? addedStudyProgram = await _institutionRepo.GetStudyProgram(studyProgram.Id, institution!, faculty!);

            StudyProgramDto? studyProgramToReturn = _mapper.Map<StudyProgramDto>(addedStudyProgram);
            return studyProgramToReturn;
        }
    }
}
