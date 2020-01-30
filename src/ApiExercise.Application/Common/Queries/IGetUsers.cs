using System.Threading;
using System.Threading.Tasks;
using ApiExercise.Application.Common.ResponseModels;
using ApiExercise.Application.Users.GetUsers;

namespace ApiExercise.Application.Common.Queries
{
    public interface IGetUsers
    {
        Task<PaginatedResponse<UserListItemResponseModel>> Query(GetUsersRequest getUsers, CancellationToken cancellationToken);
    }
}
