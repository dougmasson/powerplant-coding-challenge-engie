using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace PowerCalculator.Domain.Models
{
    [ExcludeFromCodeCoverage]
    [DebuggerDisplay("Name: {Name} | Power: {Power.ToString(\"N1\")}")]
    public sealed record ProductionPlan
    {
        public string Name { get; init; }
        public double Power { get; init; }
    }
}
