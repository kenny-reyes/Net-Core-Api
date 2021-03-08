using System.Threading;
using System.Threading.Tasks;
using NetCoreApiScaffolding.Application.Common;
using NetCoreApiScaffolding.Application.ResponseModels;
using NetCoreApiScaffolding.Application.Users.GetUsers;

namespace NetCoreApiScaffolding.Application.Interfaces.Queries
{
    public interface IGetUsers
    {
        Task<PaginatedResponse<UserListItemResponseModel>> Query(GetUsersRequest getUsers, CancellationToken cancellationToken);
    }
}