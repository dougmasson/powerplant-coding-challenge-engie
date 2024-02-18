using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace PowerCalculator.Application.Models.Request
{
    [ExcludeFromCodeCoverage]
    public sealed record Fuels
    {
        [JsonPropertyName("gas(euro/MWh)")]
        public double GasCost { get; init; }

        [JsonPropertyName("kerosine(euro/MWh)")]
        public double KerosineCost { get; init; }

        [JsonPropertyName("co2(euro/ton)")]
        public double Co2Cost { get; init; }

        [JsonPropertyName("wind(%)")]
        public double WindEfficiency { get; init; }
    }
}