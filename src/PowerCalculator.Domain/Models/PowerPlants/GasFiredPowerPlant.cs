using PowerCalculator.Domain.Enums;
using System.Diagnostics.CodeAnalysis;

namespace PowerCalculator.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public sealed class GasFiredPowerPlant : PowerPlant
    {
        public override FuelType Type => FuelType.Gas;

        public override void CalculatePowerCost(FuelsInfo fuelsInfo)
        {
            PowerCost = (fuelsInfo.GasCost) / Efficiency
                          + (fuelsInfo.CarbonConsumptionCost * fuelsInfo.CarbonEmissionByGasfired);
        }

        public override void CalculatePowerCapacityToGenerate(FuelsInfo fuelInfo)
        {
            PowerCapacityToGenerate = PowerMaximum;
        }
    }
}
