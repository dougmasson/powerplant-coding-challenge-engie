using PowerCalculator.Domain.Models;

namespace PowerCalculator.Application.Factory
{
    /// <summary>
    /// Declares the factory method, which returns an new object inehtan of <see cref="PowerPlant"/>.
    /// </summary>
    internal interface IPowerPlantFactory
    {
        /// <summary>
        /// Create a powerPlant based on informations of <see cref="PowerPlantInfo"/> and <see cref="FuelsInfo"/>
        /// </summary>
        /// <param name="PowerPlantInfo"></param>
        /// <param name="fuelsInfo"></param>
        /// <returns>Instance of concrete implementation of the class.</returns>
        public PowerPlant Create(PowerPlantInfo PowerPlantInfo, FuelsInfo fuelsInfo);
    }
}
