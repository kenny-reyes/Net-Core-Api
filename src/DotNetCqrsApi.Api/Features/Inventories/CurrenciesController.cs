using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCqrsApi.Api.Features.Inventories
{
    [ApiController]
    [ApiVersion(ApiConstants.ApiVersionV1)]
    [Route(ApiConstants.BaseApiRoute + "/currencies")]
    public class CurrenciesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CurrenciesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(ApiConstants.AllCurrencies)]
        [ProducesResponseType(typeof(PaginatedResponse<CurrencyModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<PaginatedResponse<CurrencyModel>>> GetCurrencies([FromBody]GetCurrenciesRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(CurrencyModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<CurrencyModel>> GetCurrency([FromQuery]GetCurrencyByIdRequest request)
        {
            var currency = await _mediator.Send(request);
            return Ok(currency);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CurrencyModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<CurrencyModel>> CreateCurrency([FromBody]CreateCurrencyRequest request)
        {
            var currency = await _mediator.Send(request);
            return Ok(currency);
        }

        [HttpPut]
        [ProducesResponseType(typeof(CurrencyModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<CurrencyModel>> UpdateCurrency([FromBody]UpdateCurrencyRequest request)
        {
            var currency = await _mediator.Send(request);
            return Ok(currency);
        }

        [HttpGet(ApiConstants.EmailAlreadyExists)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<bool>> IsoCodeAlreadyExists([FromQuery]IsoCodeAlreadyExistsRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
