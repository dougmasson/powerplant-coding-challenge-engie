using AutoMapper;
using PowerCalculator.Application.Models.Request;
using PowerCalculator.Application.Models.Response;
using PowerCalculator.Domain.Common;
using PowerCalculator.Domain.Enums;
using PowerCalculator.Domain.Models;

namespace PowerCalculator.Application.Mappings
{
    public class ProductionPlanMappingProfile : Profile
    {
        public ProductionPlanMappingProfile()
        {
            CreateMap<Models.Request.PowerPlant, PowerPlantInfo>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.GetEnumFromMemberAttrValue<FuelType>()))
                .ForMember(dest => dest.PowerMinimum, opt => opt.MapFrom(src => src.PMin))
                .ForMember(dest => dest.PowerMaximum, opt => opt.MapFrom(src => src.PMax));

            CreateMap<Fuels, FuelsInfo>()
                .ForMember(dest => dest.CarbonConsumptionCost, opt => opt.MapFrom(src => src.Co2Cost));

            CreateMap<PowerPlantInfo, Domain.Models.PowerPlant>();

            CreateMap<ProductionPlan, ProductionPlanResponse>();
        }
    }
}
