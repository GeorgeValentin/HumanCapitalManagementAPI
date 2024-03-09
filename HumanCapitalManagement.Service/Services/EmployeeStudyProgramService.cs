using AutoMapper;
using FluentValidation;
using HumanCapitalManagement.Domain.Models;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;
using HumanCapitalManagement.Entities.DTOs.StudyProgramDTOs;
using HumanCapitalManagement.Persistance.Repositories;

namespace HumanCapitalManagement.Service.Services
{
    public class EmployeeStudyProgramService : IEmployeeStudyProgramService
    {
        private readonly IInstitutionRepo _institutionRepo;
        private readonly IEmployeeStudyProgramRepo _employeeStudyProgramRepo;
        private readonly IEntitiesRepo _entitiesRepo;
        private readonly IMapper _mapper;
        private readonly IEmployeeRepo _employeeRepo;
        private readonly IValidator<CreateStudyProgramForEmployeeValidatorDto> _createStudyProgramForEmployeeValidator;
        private readonly IValidator<EmployeeExistanceValidatorDto> _employeeExistanceValidator;
        private readonly IValidator<StudyProgramExistanceValidatorDto> _studyProgramExistanceValidator;

        public EmployeeStudyProgramService(
            IInstitutionRepo institutionRepo, 
            IEmployeeStudyProgramRepo employeeStudyProgramRepo, 
            IEntitiesRepo entitiesRepo, 
            IMapper mapper, 
            IEmployeeRepo employeeRepo,
            IValidator<CreateStudyProgramForEmployeeValidatorDto> createStudyProgramForEmployeeValidator,
            IValidator<EmployeeExistanceValidatorDto> employeeExistanceValidator,
            IValidator<StudyProgramExistanceValidatorDto> studyProgramExistanceValidator)
        {
            _institutionRepo = institutionRepo ?? throw new ArgumentNullException(nameof(institutionRepo));
            _employeeStudyProgramRepo = employeeStudyProgramRepo ?? throw new ArgumentNullException(nameof(employeeStudyProgramRepo));
            _entitiesRepo = entitiesRepo ?? throw new ArgumentNullException(nameof(entitiesRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _employeeRepo = employeeRepo ?? throw new ArgumentNullException(nameof(employeeRepo));
            _createStudyProgramForEmployeeValidator = createStudyProgramForEmployeeValidator ?? throw new ArgumentNullException(nameof(createStudyProgramForEmployeeValidator));
            _employeeExistanceValidator = employeeExistanceValidator ?? throw new ArgumentNullException(nameof(employeeExistanceValidator));
            _studyProgramExistanceValidator = studyProgramExistanceValidator ?? throw new ArgumentNullException(nameof(studyProgramExistanceValidator));
        }

        public async Task<EmployeeDto> AddStudyProgramToEmployee(int employeeId, int studyProgramId)
        {
            Employee? employee = await _employeeRepo.GetEmployee(employeeId);
            await _employeeExistanceValidator.ValidateAndThrowAsync(
                new EmployeeExistanceValidatorDto { Employee = employee, EmployeeId = employeeId });

            StudyProgram? studyProgram = await _institutionRepo.GetStudyProgram(studyProgramId);
            await _studyProgramExistanceValidator.ValidateAndThrowAsync(
                new StudyProgramExistanceValidatorDto { StudyProgram = studyProgram });

            await _createStudyProgramForEmployeeValidator.ValidateAndThrowAsync(
                new CreateStudyProgramForEmployeeValidatorDto { EmployeeId = employeeId, StudyProgramId = studyProgramId });

            EmployeeStudyProgram employeeStudyProgram = new EmployeeStudyProgram { EmployeeId = employeeId, StudyProgramId = studyProgramId};

            await _employeeStudyProgramRepo.AddEmployeeStudyProgram(employeeStudyProgram);
            await _entitiesRepo.SaveChanges();

            employee!.EmployeeStudyProgramId = employeeStudyProgram.Id;

            _employeeRepo.UpdateEmployee(employee);
            await _entitiesRepo.SaveChanges();

            EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }

        public async Task<StudyProgramDto?> GetStudyProgramForEmployee(int employeeId)
        {
            Employee? employee = await _employeeRepo.GetEmployee(employeeId);
            await _employeeExistanceValidator.ValidateAndThrowAsync(
                new EmployeeExistanceValidatorDto { Employee = employee, EmployeeId = employeeId });

            StudyProgram? studyProgram = await _institutionRepo.GetStudyProgramForEmployee(employeeId);
            StudyProgramDto studyProgramToReturn = _mapper.Map<StudyProgramDto>(studyProgram);

            return studyProgramToReturn;
        }
    }
}
