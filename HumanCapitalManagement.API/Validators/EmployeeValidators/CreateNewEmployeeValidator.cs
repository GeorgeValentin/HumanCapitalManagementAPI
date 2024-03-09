using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;

namespace HumanCapitalManagement.API.Validators.EmployeeValidators;

public class CreateNewEmployeeValidator : EmployeeBaseValidator<EmployeeForCreationValidatorDto>
{
    public CreateNewEmployeeValidator(ApplicationDbContext context) 
        :base(context)
    { }
}
