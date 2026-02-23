using FluentValidation;

namespace Application.CQRS.Commands.Employees.Create
{
    public class CreateEmployeeCommandValidator
        : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(100).WithMessage("Full name cannot exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not valid.")
                .MaximumLength(100);

            RuleFor(x => x.Phone)
                .MaximumLength(10).WithMessage("Phone cannot exceed 10 characters.");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Today)
                .WithMessage("DateOfBirth must be in the past");

            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage("Department is required.");

            RuleFor(x => x.PositionId)
                .NotEmpty().WithMessage("Position is required.");
        }
    }
}
