using PowerCalculator.Domain.Enums;
using System.Diagnostics.CodeAnalysis;

namespace PowerCalculator.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public sealed class TurboJetPowerPlant : PowerPlant
    {
        public override FuelType Type => FuelType.Kerosine;

        public override void CalculatePowerCost(FuelsInfo fuelsInfo)
        {
            PowerCost = fuelsInfo.KerosineCost / Efficiency;
        }

        public override void CalculatePowerCapacityToGenerate(FuelsInfo fuelsInfo)
        {
            PowerCapacityToGenerate = PowerMaximum;
        }
    }
}
