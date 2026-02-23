using FluentValidation;

namespace Application.CQRS.Commands.Positions.Create
{
    public class CreatePositionValidator
        : AbstractValidator<CreatePositionCommand>
    {
        public CreatePositionValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Position name is required")
                .MaximumLength(150).WithMessage("Position name must not exceed 150 characters");
        }
    }
}
