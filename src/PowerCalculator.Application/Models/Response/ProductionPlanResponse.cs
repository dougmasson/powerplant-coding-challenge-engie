using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace PowerCalculator.Application.Models.Response
{
    [ExcludeFromCodeCoverage]
    [DebuggerDisplay("Name: {Name} | Power: {Power.ToString(\"N2\")}")]
    public sealed record ProductionPlanResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;

        [JsonPropertyName("p")]
        public double Power { get; init; }
    }
}
