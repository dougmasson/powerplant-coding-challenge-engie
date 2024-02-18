using FluentValidation;
using PowerCalculator.Application.Constants;
using PowerCalculator.Application.Models.Request;

namespace PowerCalculator.Application.Validator
{
    public class FuelsValidator : AbstractValidator<Fuels>
    {
        public FuelsValidator()
        {
            RuleFor(x => x.GasCost)
                .NotNull().WithMessage(Messages.FIELD_REQUIRED)
                .OverridePropertyName("gas(euro/MWh)")
                .GreaterThanOrEqualTo(0).WithMessage(Messages.FIELD_GREATER_EQUAL_ZERO);

            RuleFor(x => x.KerosineCost)
                .NotNull().WithMessage(Messages.FIELD_REQUIRED)
                .OverridePropertyName("kerosine(euro/MWh)")
                .GreaterThanOrEqualTo(0).WithMessage(Messages.FIELD_GREATER_EQUAL_ZERO);

            RuleFor(x => x.Co2Cost)
                .NotNull().WithMessage(Messages.FIELD_REQUIRED)
                .OverridePropertyName("co2(euro/ton)")
                .GreaterThanOrEqualTo(0).WithMessage(Messages.FIELD_GREATER_EQUAL_ZERO);

            RuleFor(x => x.WindEfficiency)
                .NotNull().WithMessage(Messages.FIELD_REQUIRED)
                .OverridePropertyName("wind(%)")
                .GreaterThanOrEqualTo(0).WithMessage(Messages.FIELD_GREATER_EQUAL_ZERO);
        }
    }
}
