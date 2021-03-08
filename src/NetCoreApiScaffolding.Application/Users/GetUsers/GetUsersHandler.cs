using System.Threading;
using System.Threading.Tasks;
using NetCoreApiScaffolding.Application.Common;
using MediatR;
using NetCoreApiScaffolding.Application.Interfaces.Queries;
using NetCoreApiScaffolding.Application.ResponseModels;

namespace NetCoreApiScaffolding.Application.Users.GetUsers
{
    public class GetUsersHandler : IRequestHandler<GetUsersRequest, PaginatedResponse<UserListItemResponseModel>>
    {
        private readonly IGetUsers _getUsersQueries;

        public GetUsersHandler(IGetUsers getUsersQueries)
        {
            _getUsersQueries = getUsersQueries;
        }

        public async Task<PaginatedResponse<UserListItemResponseModel>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            return await _getUsersQueries.Query(request, cancellationToken);
        }
    }
}