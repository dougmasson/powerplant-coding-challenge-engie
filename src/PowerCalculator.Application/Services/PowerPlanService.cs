using Microsoft.Extensions.Logging;
using PowerCalculator.Application.Factory;
using PowerCalculator.Application.Interfaces;
using PowerCalculator.Application.Interfaces.Infrastructure;
using PowerCalculator.Domain.Exceptions;
using PowerCalculator.Domain.Models;

namespace PowerCalculator.Application.Services
{
    internal class PowerPlanService : IPowerPlanService
    {
        private readonly IPowerPlantFactory _powerPlantFactory;
        private readonly IConfigurationService _configurationService;
        private readonly ILogger<PowerPlanService> _logger;

        public PowerPlanService(IPowerPlantFactory powerPlantFactory, IConfigurationService configurationService, ILogger<PowerPlanService> logger)
        {
            _powerPlantFactory = powerPlantFactory;
            _configurationService = configurationService;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<List<ProductionPlan>> CreateProductionPlanAsync(double load, List<PowerPlantInfo> powerPlantInfos, FuelsInfo fuelInfo)
        {
            var powerPlants = await CreatePowerPlantAsync(powerPlantInfos, fuelInfo);

            SortByMeritOrder(ref powerPlants);

            var productionPlans = await ComputeProductionPlanAsync(powerPlants, load);

            return productionPlans;
        }

        /// <summary>
        /// Create powerplants based on informations of <see cref="PowerPlantInfo"/> and <see cref="FuelsInfo"/>.
        /// </summary>
        /// <param name="powerPlantInfos">Information of powerplant.</param>
        /// <param name="fuelsInfo">Information of the fuels of each powerplant.</param>
        /// <returns>List of <see cref="PowerPlant"/> to compute production plan.</returns>
        internal async Task<List<PowerPlant>> CreatePowerPlantAsync(List<PowerPlantInfo> powerPlantInfos, FuelsInfo fuelsInfo)
        {
            _logger.LogInformation("Create powerplant");

            fuelsInfo.CarbonEmissionByGasfired = await _configurationService.GetCarbonEmissionGasfiredPowerplantAsync();

            var powerPlants = new List<PowerPlant>();

            foreach (var item in powerPlantInfos)
            {
                var powerPlant = _powerPlantFactory.Create(item, fuelsInfo);
                powerPlants.Add(powerPlant);
            }

            return powerPlants;
        }

        /// <summary>
        /// Ranking available sources of energy based on ascending order of price and power able to generate.
        /// </summary>
        /// <param name="powerPlants">List of <see cref="PowerPlant"/> at disposal to generate the power.</param>
        /// <returns>Ordered list.</returns>
        internal void SortByMeritOrder(ref List<PowerPlant> powerPlants)
        {
            _logger.LogInformation("Sort by merit-order");

            var orderByPowerPlantAbleToGenerate = powerPlants.Where(x => x.PowerCapacityToGenerate > 0)
                                                             .OrderBy(x => x.PowerCost)
                                                             .ThenBy(i => i.PowerMinimum);

            var orderByPowerPlantNotAbleToGenerate = powerPlants.Where(x => x.PowerCapacityToGenerate == 0)
                                                                .OrderBy(x => x.PowerCost)
                                                                .ThenBy(i => i.PowerMinimum);

            powerPlants = orderByPowerPlantAbleToGenerate.Concat(orderByPowerPlantNotAbleToGenerate)
                                                         .ToList();
        }

        /// <summary>
        /// Calculate how much energy needs to produce for each powerplant.
        /// </summary>
        /// <param name="powerPlants">List of <see cref="PowerPlant"/> at disposal to generate the power.</param>
        /// <param name="load">Amount of energy that need to be generated.</param>
        internal async Task<List<ProductionPlan>> ComputeProductionPlanAsync(List<PowerPlant> powerPlants, double load)
        {
            _logger.LogInformation("Compute production plan");

            double remainingLoad = load;

            for (int i = 0; i < powerPlants.Count; i++)
            {
                _logger.LogInformation($"Powerplan: {powerPlants[i].Name}");

                bool existsMinimumPowerEnough = remainingLoad >= powerPlants[i].PowerMinimum;

                if (!existsMinimumPowerEnough)
                {
                    var newRemainingLoad = await RedistributePowerPlanGeneratedAsync(powerPlants, remainingLoad, i);

                    if (!powerPlants[i].CanOperate)
                    {
                        RollbackRedistributePowerPlanGenerated(powerPlants, i);
                        continue;
                    }
                    else
                    {
                        remainingLoad = newRemainingLoad;
                    }
                }

                remainingLoad = await CalculatePowerGeneratedForPowerPlanAsync(powerPlants[i], remainingLoad);

                if (remainingLoad == 0)
                {
                    break;
                }
            }

            if (powerPlants.Sum(i => i.PowerGeneratedForPlan) != load)
            {
                throw new LoadExceedsMaxProductionException();
            }

            return BuildProductionPlan(powerPlants);
        }

        /// <summary>
        /// Calculate power generated for Powerplan.
        /// </summary>
        /// <param name="powerPlant"></param>
        /// <param name="remainingLoad"></param>
        /// <returns>Value of <c>remainingLoad</c> after calcule.</returns>
        private Task<double> CalculatePowerGeneratedForPowerPlanAsync(PowerPlant powerPlant, double remainingLoad)
        {
            if (powerPlant.PowerCapacityToGenerate == 0)
            {
                return Task.FromResult(remainingLoad);
            }

            if (remainingLoad >= powerPlant.PowerCapacityToGenerate)
            {
                powerPlant.PowerGeneratedForPlan = powerPlant.PowerCapacityToGenerate;
            }
            else
            {
                powerPlant.PowerGeneratedForPlan = remainingLoad;
            }

            powerPlant.CanOperate = true;

            remainingLoad -= powerPlant.PowerGeneratedForPlan;

            return Task.FromResult(remainingLoad);
        }

        /// <summary>
        /// Create new instance of <see cref="ProductionPlan"/> based on if powerplant can operate.
        /// </summary>
        /// <param name="powerPlants">List of <see cref="PowerPlant"/> at disposal to generate the power.</param>
        /// <returns>List of ProductionPlan created.</returns>
        private List<ProductionPlan> BuildProductionPlan(List<PowerPlant> powerPlants)
        {
            _logger.LogInformation("Create productionplan");

            // ReOrder powerplants by those can be operated
            var powerPlantsOrder = powerPlants.OrderByDescending(x => x.CanOperate)
                                              .ThenBy(i => i.PowerCost)
                                              .ThenBy(i => i.PowerMinimum);

            var productionPlans = new List<ProductionPlan>();

            foreach (var powerPlant in powerPlantsOrder)
            {
                productionPlans.Add(new ProductionPlan(powerPlant.Name, Math.Round(powerPlant.PowerGeneratedForPlan, 1)));
            }

            return productionPlans;
        }

        /// <summary>
        /// Redistribute power generation plan to able current <see cref="PowerPlant"/> has minimum power to operate.
        /// </summary>
        /// <param name="powerPlants">List of <see cref="PowerPlant"/> at disposal to generate the power.</param>
        /// <param name="remainingLoad">Remaining Load.</param>
        /// <param name="indexCurrentPowerPlant">Index of current <see cref="PowerPlant"/>.</param>
        /// <returns>Value of <c>remainingLoad</c> after recalcule.</returns>
        private Task<double> RedistributePowerPlanGeneratedAsync(List<PowerPlant> powerPlants, double remainingLoad, int indexCurrentPowerPlant)
        {
            _logger.LogInformation("Redistribute powerplan generated");

            var powerDeficit = powerPlants[indexCurrentPowerPlant].PowerMinimum - remainingLoad;

            for (int j = indexCurrentPowerPlant - 1; j >= 0; j--)
            {
                // Save current value in case need to call RollbackRedistributePowerPlanGenerated method
                powerPlants[j].PowerGeneratedForPlanOld = powerPlants[j].PowerGeneratedForPlan;

                var powerToSubtract = powerPlants[j].PowerGeneratedForPlan - powerDeficit;

                bool isPowerEnoughToSubtract = powerToSubtract > powerPlants[j].PowerMinimum
                                                    && powerToSubtract < powerPlants[j].PowerGeneratedForPlan;

                if (isPowerEnoughToSubtract)
                {
                    powerPlants[j].PowerGeneratedForPlan -= powerDeficit;

                    remainingLoad += powerDeficit;
                    powerDeficit = 0;
                }
                else
                {
                    var powerExcess = powerPlants[j].PowerGeneratedForPlan - powerPlants[j].PowerMinimum;
                    powerPlants[j].PowerGeneratedForPlan = powerPlants[j].PowerMinimum;

                    remainingLoad += powerExcess;
                    powerDeficit -= powerExcess;
                }

                if (powerDeficit == 0)
                {
                    powerPlants[indexCurrentPowerPlant].CanOperate = true;
                    break;
                }
            }

            return Task.FromResult(remainingLoad);
        }

        /// <summary>
        /// Rollback of power was redistributed to the current <see cref="PowerPlant"/> has minimum power to operate.
        /// </summary>
        /// <param name="powerPlants">List of <see cref="PowerPlant"/> at disposal to generate the power.</param>
        /// <param name="i">Index of current <see cref="PowerPlant"/>.</param>
        private void RollbackRedistributePowerPlanGenerated(List<PowerPlant> powerPlants, int i)
        {
            _logger.LogInformation("Rollback redistribute powerplan generated");

            for (int j = i - 1; j >= 0; j--)
            {
                powerPlants[j].PowerGeneratedForPlan = powerPlants[j].PowerGeneratedForPlanOld;
                powerPlants[j].PowerGeneratedForPlanOld = 0;
            }
        }
    }
}
