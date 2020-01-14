using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCqrsApi.Api.Features.Inventories
{
    [ApiController]
    [ApiVersion(ApiConstants.ApiVersionV1)]
    [Route(ApiConstants.BaseApiRoute + "/facilities")]
    public class FacilitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FacilitiesController(IMediator mediator) => _mediator = mediator;

        [HttpPost(ApiConstants.AllFacilities)]
        [ProducesResponseType(typeof(PaginatedResponse<FacilityModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<PaginatedResponse<FacilityModel>>> GetFacilities([FromBody]GetFacilitiesRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(FacilityModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<FacilityModel>> GetFacility([FromQuery]GetFacilityByIdRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(FacilityModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<FacilityModel>> CreateFacility([FromBody]CreateFacilityRequest request)
        {
            var facilityModel = await _mediator.Send(request);
            return Ok(facilityModel);
        }
        
        [HttpPut]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult> UpdateFacility([FromBody]UpdateFacilityRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPut(ApiConstants.DisableFacility)]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult> DisableFacility([FromBody]DisableFacilityRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPut(ApiConstants.EnableFacility)]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult> EnableFacility([FromBody]EnableFacilityRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpGet(ApiConstants.NameAlreadyExists)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<bool>> NameAlreadyExists([FromQuery]NameAlreadyExistsRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
