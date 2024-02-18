namespace PowerCalculator.Application.Interfaces.Infrastructure
{
    public interface IConfigurationService
    {
        /// <summary>
        /// Get quantity of carbon emission of gas-fired powerplant.
        /// </summary>
        /// <returns>Value in tonne of carbon emission each MWh.</returns>
        Task<double> GetCarbonEmissionGasfiredPowerplantAsync();
    }
}
