using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCqrsApi.Api.Features.Inventories
{
    [ApiController]
    [ApiVersion(ApiConstants.ApiVersionV1)]
    [Route(ApiConstants.BaseApiRoute + "/companies")]
    public class CompaniesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompaniesController(IMediator mediator) => _mediator = mediator;

        [HttpPost(ApiConstants.AllCompanies)]
        [ProducesResponseType(typeof(PaginatedResponse<CompanyModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<PaginatedResponse<CompanyModel>>> GetCompanies([FromBody] GetCompaniesRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(CompanyModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<CompanyModel>> GetCompany([FromQuery]GetCompanyByIdRequest request)
        {
            var company = await _mediator.Send(request);
            return Ok(company);
        }        

        [HttpPost]
        [ProducesResponseType(typeof(CompanyModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<CompanyModel>> CreateCompany([FromBody] CreateCompanyRequest request)
        {
            var company = await _mediator.Send(request);
            return Ok(company);
        }

        [HttpPut]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult> UpdateCompany([FromBody]UpdateCompanyRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPut(ApiConstants.DisableCompany)]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult> DisableCompany([FromBody]int id)
        {
            await _mediator.Send(new DisableCompanyRequest { Id = id });
            return Ok();
        }

        [HttpPut(ApiConstants.EnableCompany)]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult> EnableCompany([FromBody]int id)
        {
            await _mediator.Send(new EnableCompanyRequest { Id = id });
            return Ok();
        }
    }
}
