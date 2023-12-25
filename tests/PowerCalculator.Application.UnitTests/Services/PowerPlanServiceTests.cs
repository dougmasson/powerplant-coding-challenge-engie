using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using PowerCalculator.Application.Factory;
using PowerCalculator.Application.Interfaces.Infrastructure;
using PowerCalculator.Application.Mappings;
using PowerCalculator.Application.Services;
using PowerCalculator.Domain.Enums;
using PowerCalculator.Domain.Models;
using Xunit;

namespace PowerCalculator.Application.UnitTests
{
    public class PowerPlanServiceTests
    {
        private readonly IMapper _mapper;

        public PowerPlanServiceTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ProductionPlanMappingProfile>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task CreateProductionPlan_TooMuchPowerProduced_ProductionPlanGenerated()
        {
            // Arrage
            var expected = new List<ProductionPlan>
            {
                new ProductionPlan("gasfiredbig1", 380d),
                new ProductionPlan("gasfiredbig2", 100d),
                new ProductionPlan("gasfiredsomewhatsmaller", 0d),
                new ProductionPlan("tj1", 0d),
                new ProductionPlan("windpark1", 0d),
                new ProductionPlan("windpark2", 0d)
            };

            var powerPlantInfos = new List<PowerPlantInfo>
            {
                new PowerPlantInfo
                {
                    Name = "gasfiredbig1",
                    Type = FuelType.Gas,
                    Efficiency = 0.53d,
                    PowerMinimum = 100d,
                    PowerMaximum = 460d
                },
                new PowerPlantInfo
                {
                    Name = "gasfiredbig2",
                    Type = FuelType.Gas,
                    Efficiency = 0.53d,
                    PowerMinimum = 100d,
                    PowerMaximum = 460d
                },
                new PowerPlantInfo
                {
                    Name = "gasfiredsomewhatsmaller",
                    Type = FuelType.Gas,
                    Efficiency = 0.37d,
                    PowerMinimum = 40d,
                    PowerMaximum = 210d
                },
                new PowerPlantInfo
                {
                    Name = "tj1",
                    Type = FuelType.Kerosine,
                    Efficiency = 0.3d,
                    PowerMinimum = 0d,
                    PowerMaximum = 16d
                },
                new PowerPlantInfo
                {
                    Name = "windpark1",
                    Type = FuelType.Wind,
                    Efficiency = 1d,
                    PowerMinimum = 0d,
                    PowerMaximum = 150d
                },
                new PowerPlantInfo
                {
                    Name = "windpark2",
                    Type = FuelType.Wind,
                    Efficiency = 1d,
                    PowerMinimum = 0d,
                    PowerMaximum = 36d
                }
            };

            var fuelsInfo = new FuelsInfo
            {
                GasCost = 13.4d,
                KerosineCost = 50.8d,
                CarbonConsumptionCost = 20d,
                WindEfficiency = 0
            };

            var factory = new PowerPlantFactory(_mapper);
            var configurationServicesMock = new Mock<IConfigurationService>();
            var logger = new NullLogger<PowerPlanService>();

            var powerPlanService = new PowerPlanService(factory, configurationServicesMock.Object, logger);

            // Act
            var result = await powerPlanService.CreateProductionPlanAsync(480, powerPlantInfos, fuelsInfo);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }


    }
}