using AutoMapper;
using FluentAssertions;
using PowerCalculator.Application.Factory;
using PowerCalculator.Application.Mappings;
using PowerCalculator.Domain.Enums;
using PowerCalculator.Domain.Models;
using Xunit;

namespace PowerCalculator.Application.UnitTests
{
    public class PowerPlantFactoryTest
    {
        private readonly IMapper _mapper;

        public PowerPlantFactoryTest()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ProductionPlanMappingProfile>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Create_PowerPlant_GasFiredPowerPlant()
        {
            // Assert
            var expected = new GasFiredPowerPlant
            {
                Name = "gasfiredbig1",
                Efficiency = 0.53d,
                PowerMinimum = 100,
                PowerMaximum = 460,
                PowerCost = 25.28d,
                PowerCapacityToGenerate = 460,
                CanOperate = false
            };

            var powerPlantInfo = new PowerPlantInfo
            {
                Name = "gasfiredbig1",
                Type = FuelType.Gas,
                Efficiency = 0.53d,
                PowerMinimum = 100d,
                PowerMaximum = 460d
            };

            var fuelsInfo = new FuelsInfo
            {
                GasCost = 13.4d,
                KerosineCost = 50.8d,
                CarbonConsumptionCost = 20d,
                WindEfficiency = 60d
            };

            var powerPlantFactory = new PowerPlantFactory(_mapper);

            // Act
            var result = powerPlantFactory.Create(powerPlantInfo, fuelsInfo);

            // Assert
            result.Should().BeEquivalentTo(expected, options => options.Using<double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, 0.2))
                                                                       .WhenTypeIs<double>());
        }
    }
}
