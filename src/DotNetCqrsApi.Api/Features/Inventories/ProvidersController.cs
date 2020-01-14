using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCqrsApi.Api.Features.Inventories
{
    [ApiController]   
    [ApiVersion(ApiConstants.ApiVersionV1)]
    [Route(ApiConstants.BaseApiRoute + "/providers")]
    public class ProvidersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProvidersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(ApiConstants.AllProviders)]
        [ProducesResponseType(typeof(PaginatedResponse<ProviderListItemModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<PaginatedResponse<ProviderListItemModel>>> GetProviders([FromBody]GetProvidersRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProviderModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult> GetProvider([FromQuery]GetProviderRequest request)
        {
            var provider = await _mediator.Send(request);
            return Ok(provider);
        }        

        [HttpPost]
        [ProducesResponseType(typeof(ProviderModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult> CreateProvider([FromBody]CreateProviderRequest request)
        {
            var provider = await _mediator.Send(request);
            return Ok(provider);
        }

        [HttpPut]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult> UpdateProvider([FromBody]UpdateProviderRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPut(ApiConstants.EnableProvider)]
        [ProducesResponseType(typeof(ProviderModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult> EnableProvider([FromBody]EnableProviderRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPut(ApiConstants.DisableProvider)]
        [ProducesResponseType(typeof(ProviderModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult> DisableProvider([FromBody]DisableProviderRequest request)
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

        [HttpGet(ApiConstants.AcronymAlreadyExists)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<bool>> AcronymAlreadyExists([FromQuery]AcronymAlreadyExistsRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
