using PowerCalculator.Domain.Enums;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace PowerCalculator.Domain.Models
{
    [ExcludeFromCodeCoverage]
    [DebuggerDisplay("Name: {Name} | Type: {Type} | Pmin: {PowerMinimum.ToString(\"N2\")} | Pmax: {PowerMaximum.ToString(\"N2\")}")]
    public sealed record PowerPlantInfo
    {
        public string Name { get; init; } = string.Empty;
        public FuelType Type { get; init; }
        public double Efficiency { get; init; }
        public double PowerMinimum { get; init; }
        public double PowerMaximum { get; init; }
    }
}