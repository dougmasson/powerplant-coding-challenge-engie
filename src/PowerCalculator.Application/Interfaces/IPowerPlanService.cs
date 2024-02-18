using PowerCalculator.Domain.Models;

namespace PowerCalculator.Application.Interfaces
{
    public interface IPowerPlanService
    {
        /// <summary>
        /// Create a production plan to specify for each Powerplant how much power each powerplant should deliver.
        /// </summary>
        /// <param name="load">Demand of power.</param>
        /// <param name="powerPlantInfos">Information of powerplant.</param>
        /// <param name="fuelsInfo">Information of the fuels of each powerplant.</param>
        /// <returns>Value of <see cref="ProductionPlan"/> generated.</returns>
        public Task<List<ProductionPlan>> CreateProductionPlanAsync(double load, List<PowerPlantInfo> powerPlantInfos, FuelsInfo fuelsInfo);
    }
}