using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NetCoreApiScaffolding.Application.Interfaces.Queries;
using NetCoreApiScaffolding.Application.ResponseModels;

namespace NetCoreApiScaffolding.Application.Users.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, UserResponseModel>
    {
        private readonly IGetUserById _getUsersQueries;

        public GetUserByIdHandler(IGetUserById getUsersQueries)
        {
            _getUsersQueries = getUsersQueries;
        }

        public async Task<UserResponseModel> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            return await _getUsersQueries.Query(request.Id, cancellationToken);
        }
    }
}