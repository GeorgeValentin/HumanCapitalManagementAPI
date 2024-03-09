using HumanCapitalManagement.Domain.Data;
using HumanCapitalManagement.Entities.DTOs.EmployeeDTOs;

namespace HumanCapitalManagement.API.Validators.EmployeeValidators;

public class UpdateEmployeeValidator : EmployeeBaseValidator<EmployeeForUpdateValidatorDto>
{
    public UpdateEmployeeValidator(ApplicationDbContext context) 
        :base(context)
    { }
}
