using ApiExercise.Application.Common;
using ApiExercise.Application.Common.ResponseModels;
using MediatR;

namespace ApiExercise.Application.Users.GetUsers
{
    public class GetUsersRequest : PaginatedRequest, IRequest<PaginatedResponse<UserListItemResponseModel>>
    { }
}
