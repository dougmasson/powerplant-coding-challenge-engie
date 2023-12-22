using PowerCalculator.Domain.Enums;
using System.Diagnostics.CodeAnalysis;

namespace PowerCalculator.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public sealed class WindTurbinePowerPlant : PowerPlant
    {
        public override FuelType Type => FuelType.Wind;

        public override void CalculatePowerCost(FuelsInfo fuelsInfo)
        {
            PowerCost = 0;
        }

        public override void CalculatePowerCapacityToGenerate(FuelsInfo fuelsInfo)
        {
            PowerCapacityToGenerate = (fuelsInfo.WindEfficiency / 100) * PowerMaximum;
        }
    }
}
