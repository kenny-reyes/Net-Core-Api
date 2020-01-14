using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCqrsApi.Api.Features.Inventories
{
    [ApiController]
    [ApiVersion(ApiConstants.ApiVersionV1)]
    [Route(ApiConstants.BaseApiRoute + "/operation-businesses")]
    public class OperationBusinessesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OperationBusinessesController(IMediator mediator) => _mediator = mediator;

        [HttpPost(ApiConstants.AllOperationBusinesses)]
        [ProducesResponseType(typeof(PaginatedResponse<OperationBusinessListItemModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<PaginatedResponse<OperationBusinessListItemModel>>> GetOprationBusinesses([FromBody]GetOperationBusinessesRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(OperationBusinessModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult> GetOperationBusiness([FromQuery]GetOperationBusinessRequest request)
        {
            var operationBusiness = await _mediator.Send(request);
            return Ok(operationBusiness);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OperationBusinessModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult> CreateOperationBusiness([FromBody]CreateOperationBusinessRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult> UpdateOperationBusiness([FromBody]UpdateOperationBusinessRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }        

        [HttpPut(ApiConstants.DisableOperationBusiness)]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult> DisableOperationBusiness([FromBody]DisableOperationBusinessRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPut(ApiConstants.EnableOperationBusiness)]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult> EnableOperationBusiness([FromBody]EnableOperationBusinessRequest request)
        {
            await _mediator.Send(request);
            return Ok();
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
