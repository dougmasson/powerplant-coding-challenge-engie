using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace PowerCalculator.Application.Models.Request
{
    [ExcludeFromCodeCoverage]
    [DebuggerDisplay("Name: {Name} | Type: {Type} | Efficiency: {Efficiency.ToString(\"N2\")} | Pmin: {Pmin.ToString(\"N2\") | Pmax: {Pmin.ToString(\"N2\")}")]
    public sealed record PowerPlant
    {
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        [JsonPropertyName("type")]
        public string? Type { get; init; }

        [JsonPropertyName("efficiency")]
        public double? Efficiency { get; init; }

        [JsonPropertyName("pmin")]
        public double? PMin { get; init; }

        [JsonPropertyName("pmax")]
        public double? PMax { get; init; }
    }
}
