using System.Threading.Tasks;
using ApiExercise.Application.Common;
using ApiExercise.Application.Common.ResponseModels;
using ApiExercise.Application.Users.CreateUser;
using ApiExercise.Application.Users.DeleteUserById;
using ApiExercise.Application.Users.GetUserById;
using ApiExercise.Application.Users.GetUsers;
using ApiExercise.Application.Users.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiExercise.Api.Controllers
{
    [ApiController]
    [ApiVersion(ApiConstants.ApiVersionV1)]
    [Route(ApiConstants.BaseApiRoute + "/Users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator) => _mediator = mediator;

        /*
         * NOTE: Why I used Post for GETALL? I did because you can use the body with limitation, and this is very useful when
         * you have a lot of parameters. I this case we only have order and the parameters, but we can add very complex
         * filters for example.
         * If you don't like only change to [HttpGet(ApiConstants.AllUsers)] and GetUsers([FromQuery]GetUsersRequest request)
         */
        [HttpPost(ApiConstants.AllUsers)]
        [ProducesResponseType(typeof(PaginatedResponse<UserListItemResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<PaginatedResponse<UserListItemResponseModel>>> GetUsers([FromBody]GetUsersRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<UserResponseModel>> GetUserById([FromQuery]GetUserByIdRequest request)
        {
            var User = await _mediator.Send(request);
            return Ok(User);
        } 
        
        [HttpPost]
        [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<UserResponseModel>> CreateUser([FromBody]CreateUserRequest request)
        {
            var user = await _mediator.Send(request);
            return Ok(user);
        }
        
        [HttpPut]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult> UpdateUser([FromBody]UpdateUserRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        /*
         * NOTE: Why am I using the Body for the parameters? It was only an election, I don't have a favourite way.
         * I though you have more homogeneity in all the controllers, but if you neeed parameters in the url you can do this 
         *
         *     [HttpDelete("{id}")]
         *     public async Task<ActionResult> DeleteUser(int idToDelete)
         *     {
         *       await _mediator.Send(new DeleteUserByIdRequest { Id = idToDelete });
         *       return Ok();
         *     }
         * 
         * Or even set [FromQuery] in the controller (Easy!)
         */
        [HttpDelete]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult> DeleteUser([FromBody]DeleteUserByIdRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
