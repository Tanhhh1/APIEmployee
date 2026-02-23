using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Commands.Positions.Update
{
    public class UpdatePositionValidator
        : AbstractValidator<UpdatePositionCommand>
    {
        public UpdatePositionValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Position id is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Position name is required")
                .MaximumLength(150).WithMessage("Position name must not exceed 150 characters");
        }
    }
}
