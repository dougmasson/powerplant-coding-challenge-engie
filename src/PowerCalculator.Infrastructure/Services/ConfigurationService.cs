using Microsoft.Extensions.Logging;
using PowerCalculator.Application.Interfaces.Infrastructure;

namespace PowerCalculator.Infrastructure.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly ILogger<ConfigurationService> _logger;

        public ConfigurationService(ILogger<ConfigurationService> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public Task<double> GetCarbonEmissionGasfiredPowerplantAsync()
        {
            _logger.LogInformation("Load external configurations of powerplants");

            var value = 0.3;

            return Task.FromResult(value);
        }
    }
}
