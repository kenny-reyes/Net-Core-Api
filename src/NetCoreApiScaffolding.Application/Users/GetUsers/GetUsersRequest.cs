using NetCoreApiScaffolding.Application.Common;
using NetCoreApiScaffolding.Application.Common.ResponseModels;
using MediatR;

namespace NetCoreApiScaffolding.Application.Users.GetUsers
{
    public class GetUsersRequest : PaginatedRequest, IRequest<PaginatedResponse<UserListItemResponseModel>>
    {
    }
}