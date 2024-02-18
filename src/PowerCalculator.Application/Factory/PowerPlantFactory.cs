using AutoMapper;
using PowerCalculator.Domain.Common;
using PowerCalculator.Domain.Models;

namespace PowerCalculator.Application.Factory
{
    internal class PowerPlantFactory : IPowerPlantFactory
    {
        private readonly IMapper _mapper;

        public PowerPlantFactory(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public PowerPlant Create(PowerPlantInfo PowerPlantInfo, FuelsInfo fuelsInfo)
        {
            // Avoid to violate the Open-Closed principle of SOLID
            var type = Type.GetType($"PowerCalculator.Domain.Models.{PowerPlantInfo.Type.GetMemberAttrValue()}PowerPlant, PowerCalculator.Domain", true, true);
            var powerPlant = (PowerPlant)Activator.CreateInstance(type!)!;

            _mapper.Map(PowerPlantInfo, powerPlant);

            powerPlant.CalculatePower(fuelsInfo);

            return powerPlant;
        }
    }
}
