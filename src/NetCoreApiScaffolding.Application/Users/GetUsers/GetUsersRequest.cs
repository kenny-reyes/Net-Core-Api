using NetCoreApiScaffolding.Application.Common;
using MediatR;
using NetCoreApiScaffolding.Application.ResponseModels;

namespace NetCoreApiScaffolding.Application.Users.GetUsers
{
    public class GetUsersRequest : PaginatedRequest, IRequest<PaginatedResponse<UserListItemResponseModel>>
    {
    }
}