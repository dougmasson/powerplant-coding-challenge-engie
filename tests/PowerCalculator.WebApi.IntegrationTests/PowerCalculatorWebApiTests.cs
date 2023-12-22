using FluentAssertions;
using PowerCalculator.Application.Models.Request;
using PowerCalculator.Application.Models.Response;
using PowerCalculator.WebApi.IntegrationTests.Helper;
using Xunit;

namespace PowerCalculator.WebApi.IntegrationTests
{
    public class PowerCalculatorWebApiTests : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;

        public PowerCalculatorWebApiTests(ApiWebApplicationFactory factory)
        {
            _httpClient = factory.CreateDefaultClient();
        }

        [Fact]
        public async Task Post_ProductionPlan_FromPlayload3()
        {
            // Arrange
            var expected = new List<ProductionPlanResponse>
            {
                new ProductionPlanResponse
                {
                    Name = "windpark1",
                    Power = 90.0d
                },
                new ProductionPlanResponse
                {
                    Name = "windpark2",
                    Power = 21.6d
                },
                new ProductionPlanResponse
                {
                    Name = "gasfiredbig1",
                    Power = 460.0d
                },
                new ProductionPlanResponse
                {
                    Name = "gasfiredbig2",
                    Power = 338.4d
                },
                new ProductionPlanResponse
                {
                    Name = "gasfiredsomewhatsmaller",
                    Power = 0.0d
                },
                new ProductionPlanResponse
                {
                    Name = "tj1",
                    Power = 0.0d
                }
            };

            var request = new ProductionPlanRequest
            {
                Load = 910d,
                Fuels = new Fuels
                {
                    GasCost = 13.4d,
                    KerosineCost = 50.8d,
                    Co2Cost = 20d,
                    WindEfficiency = 60d
                },
                PowerPlants = new List<PowerPlant>
                {
                    new PowerPlant
                    {
                        Name = "gasfiredbig1",
                        Type = "gasfired",
                        Efficiency = 0.53d,
                        PMin = 100d,
                        PMax = 460d
                    },
                    new PowerPlant
                    {
                        Name = "gasfiredbig2",
                        Type = "gasfired",
                        Efficiency = 0.53d,
                        PMin = 100d,
                        PMax = 460d
                    },
                    new PowerPlant
                    {
                        Name = "gasfiredsomewhatsmaller",
                        Type = "gasfired",
                        Efficiency = 0.37d,
                        PMin = 40d,
                        PMax = 210d
                    },
                    new PowerPlant
                    {
                        Name = "tj1",
                        Type = "turbojet",
                        Efficiency = 0.3d,
                        PMin = 0d,
                        PMax = 16d
                    },
                    new PowerPlant
                    {
                        Name = "windpark1",
                        Type = "windturbine",
                        Efficiency = 1d,
                        PMin = 0d,
                        PMax = 150d
                    },
                    new PowerPlant
                    {
                        Name = "windpark2",
                        Type = "windturbine",
                        Efficiency = 1d,
                        PMin = 0d,
                        PMax = 36d
                    }
                }
            };

            // Act
            var result = await _httpClient.PostAndDeserialize<ProductionPlanRequest, List<ProductionPlanResponse>>("/api/v1/productionplan", request);

            // Asserts
            result.Should().BeEquivalentTo(expected);
        }
    }
}