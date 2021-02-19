using System.Threading;
using System.Threading.Tasks;
using NetCoreApiScaffolding.Application.Common.ResponseModels;
using NetCoreApiScaffolding.Application.Users.GetUsers;

namespace NetCoreApiScaffolding.Application.Common.Queries
{
    public interface IGetUsers
    {
        Task<PaginatedResponse<UserListItemResponseModel>> Query(GetUsersRequest getUsers, CancellationToken cancellationToken);
    }
}
