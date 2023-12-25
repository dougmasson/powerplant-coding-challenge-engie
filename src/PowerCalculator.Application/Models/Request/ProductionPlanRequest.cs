using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace PowerCalculator.Application.Models.Request
{
    [ExcludeFromCodeCoverage]
    public sealed record ProductionPlanRequest
    {
        [JsonPropertyName("load")]
        public double? Load { get; init; }

        [JsonPropertyName("fuels")]
        public Fuels Fuels { get; init; } = null!;

        [JsonPropertyName("powerplants")]
        public List<PowerPlant> PowerPlants { get; init; } = null!;
    }
}
