using FluentValidation;
using PowerCalculator.Application.Constants;
using PowerCalculator.Application.Models.Request;

namespace PowerCalculator.Application.Validator
{
    public class PowerPlantValidator : AbstractValidator<PowerPlant>
    {
        public PowerPlantValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage(Messages.FIELD_REQUIRED)
                .NotEmpty().WithMessage(Messages.FIELD_REQUIRED);

            RuleFor(x => x.Type)
                .NotNull().WithMessage(Messages.FIELD_REQUIRED)
                .NotEmpty().WithMessage(Messages.FIELD_REQUIRED);

            RuleFor(x => x.Efficiency)
                .NotNull().WithMessage(Messages.FIELD_REQUIRED)
                .GreaterThan(0).WithMessage(Messages.FIELD_GREATER_ZERO);

            RuleFor(x => x.PMin)
                .NotNull().WithMessage(Messages.FIELD_REQUIRED)
                .GreaterThanOrEqualTo(0).WithMessage(Messages.FIELD_GREATER_EQUAL_ZERO);

            RuleFor(x => x.PMax)
                .NotNull().WithMessage(Messages.FIELD_REQUIRED)
                .GreaterThan(0).WithMessage(Messages.FIELD_GREATER_ZERO);
        }
    }
}
