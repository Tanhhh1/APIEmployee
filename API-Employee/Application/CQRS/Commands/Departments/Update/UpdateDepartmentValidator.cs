using FluentValidation;

namespace Application.CQRS.Commands.Departments.Update
{
    public class UpdateDepartmentValidator
        : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Department id is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Department name is required")
                .MaximumLength(150).WithMessage("Department name must not exceed 150 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters");
        }
    }
}
