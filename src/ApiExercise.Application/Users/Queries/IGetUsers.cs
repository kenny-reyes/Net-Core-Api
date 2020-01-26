using System.Threading;
using System.Threading.Tasks;
using ApiExercise.Application.Shared;
using ApiExercise.Application.Users.ResponseModels;

namespace ApiExercise.Application.Users.Queries
{
    public interface IGetUsers
    {
        Task<PaginatedResponse<UserListItemModel>> Query(GetUsersRequest getUsers, CancellationToken cancellationToken);
    }
}
