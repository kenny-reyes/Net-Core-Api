using System.Threading.Tasks;
using NetCoreApiScaffolding.Application.Common;
using NetCoreApiScaffolding.Application.Common.ResponseModels;
using NetCoreApiScaffolding.Application.Users.CreateUser;
using NetCoreApiScaffolding.Application.Users.DeleteUserById;
using NetCoreApiScaffolding.Application.Users.GetUserById;
using NetCoreApiScaffolding.Application.Users.GetUsers;
using NetCoreApiScaffolding.Application.Users.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreApiScaffolding.Api.Controllers
{
    [ApiController]
    [ApiVersion(ApiConstants.ApiVersionV1)]
    [Route(ApiConstants.BaseApiRoute + "/Users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator) => _mediator = mediator;

        [HttpPost(ApiConstants.AllUsers)]
        [ProducesResponseType(typeof(PaginatedResponse<UserListItemResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<PaginatedResponse<UserListItemResponseModel>>> GetUsers([FromBody] GetUsersRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<UserResponseModel>> GetUserById([FromQuery] GetUserByIdRequest request)
        {
            var user = await _mediator.Send(request);
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<UserResponseModel>> CreateUser([FromBody] CreateUserRequest request)
        {
            var user = await _mediator.Send(request);
            return Ok(user);
        }

        [HttpPut]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult> DeleteUser([FromBody] DeleteUserByIdRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}