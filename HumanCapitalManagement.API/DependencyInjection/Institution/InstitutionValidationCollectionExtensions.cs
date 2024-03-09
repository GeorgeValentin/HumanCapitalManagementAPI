using FluentValidation;
using HumanCapitalManagement.API.Validators.EmployeeValidators;
using HumanCapitalManagement.API.Validators.InstitutionValidators;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;
using HumanCapitalManagement.Entities.DTOs.FacultyDTOs;
using HumanCapitalManagement.Entities.DTOs.InstitutionDTOs;
using HumanCapitalManagement.Entities.DTOs.StudyProgramDTOs;

namespace HumanCapitalManagement.API.DependencyInjection.Institution;

public static class InstitutionValidationCollectionExtensions
{
    public static IServiceCollection AddInstitutionValidations(this IServiceCollection services)
    {
        services.AddTransient<IValidator<CreateInstitutionValidatorDto>, CreateNewInstitutionValidator>();
        services.AddTransient<IValidator<CreateFacultyValidatorDto>, CreateNewFacultyValidator>();
        services.AddTransient<IValidator<CreateStudyProgramValidatorDto>, CreateNewStudyProgramValidator>();
        services.AddTransient<IValidator<CreateStudyProgramForEmployeeValidatorDto>, CreateNewStudyProgramForEmployeeValidator>();
        services.AddTransient<IValidator<EmployeeExistanceValidatorDto>, EmployeeExistanceValidator>();
        services.AddTransient<IValidator<InstitutionExistanceValidatorDto>, InstitutionExistanceValidator>();
        services.AddTransient<IValidator<FacultyExistanceValidatorDto>, FacultyExistanceValidator>();
        services.AddTransient<IValidator<StudyProgramExistanceValidatorDto>, StudyProgramExistanceValidator>();

        return services;
    }
}
