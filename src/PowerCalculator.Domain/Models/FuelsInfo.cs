using System.Diagnostics.CodeAnalysis;

namespace PowerCalculator.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public sealed record FuelsInfo
    {
        public double GasCost { get; init; }
        public double KerosineCost { get; init; }
        public double CarbonConsumptionCost { get; init; }
        public double WindEfficiency { get; init; }
        public double CarbonEmissionByGasfired { get; set; }
    }
}