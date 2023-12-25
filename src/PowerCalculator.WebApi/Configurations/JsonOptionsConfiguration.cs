using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace PowerCalculator.WebApi.Configurations
{
    /// <summary>
    /// Configure System.Text.Json options.
    /// </summary>
    public static class JsonOptionsConfiguration
    {
        /// <summary>
        /// Configure System.Text.Json options.
        /// </summary>
        public static Action<JsonOptions> JsonOptions() => options =>
        {
            options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        };
    }
}
