using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PowerCalculator.Application.Interfaces;
using PowerCalculator.Application.Models.Request;
using PowerCalculator.Application.Models.Response;
using PowerCalculator.Domain.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PowerCalculator.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductionPlanController : Controller
    {
        private readonly IPowerPlanService _powerPlanService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Create new instance of <see cref="ProductionPlanController"/>.
        /// </summary>
        /// <param name="powerPlanService"></param>
        /// <param name="mapper"></param>
        public ProductionPlanController(IPowerPlanService powerPlanService, IMapper mapper)
        {
            _powerPlanService = powerPlanService;
            _mapper = mapper;
        }

        /// <summary>
        /// Calculate how much power each of a multitude of different powerplants need to produce.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(List<ProductionPlanResponse>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse))]
        public async Task<IActionResult> PostAsync([FromBody] ProductionPlanRequest productionPlanInputDTO)
        {
            var powerPlants = _mapper.Map<List<PowerPlantInfo>>(productionPlanInputDTO.PowerPlants);
            var fuelsInfo = _mapper.Map<FuelsInfo>(productionPlanInputDTO.Fuels);

            var result = await _powerPlanService.CreateProductionPlanAsync(productionPlanInputDTO.Load!.Value, powerPlants, fuelsInfo);
            var productionPlan = _mapper.Map<List<ProductionPlanResponse>>(result);

            return Ok(productionPlan);
        }
    }
}
