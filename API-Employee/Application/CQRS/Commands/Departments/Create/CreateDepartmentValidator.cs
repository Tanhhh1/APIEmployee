using FluentValidation;

namespace Application.CQRS.Commands.Departments.Create
{
    public class CreateDepartmentValidator
        : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Department name is required")
                .MaximumLength(150).WithMessage("Department name must not exceed 150 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters");
        }
    }
}
