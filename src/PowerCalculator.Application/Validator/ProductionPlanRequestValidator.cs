using FluentValidation;
using PowerCalculator.Application.Constants;
using PowerCalculator.Application.Models.Request;

namespace PowerCalculator.Application.Validator
{
    public class ProductionPlanRequestValidator : AbstractValidator<ProductionPlanRequest>
    {
        public ProductionPlanRequestValidator()
        {
            RuleFor(x => x.Load)
                .NotNull().WithMessage(Messages.FIELD_REQUIRED)
                .GreaterThan(0).WithMessage(Messages.FIELD_GREATER_ZERO);

            RuleFor(x => x.Fuels)
                .NotNull().WithMessage(Messages.FIELD_REQUIRED)
                .NotEmpty().WithMessage(Messages.FIELD_REQUIRED)
                .SetValidator(new FuelsValidator());

            RuleFor(x => x.PowerPlants)
                .NotNull().WithMessage(Messages.FIELD_REQUIRED)
                .NotEmpty().WithMessage(Messages.FIELD_REQUIRED);

            RuleForEach(x => x.PowerPlants)
                .SetValidator(new PowerPlantValidator());
        }
    }
}
