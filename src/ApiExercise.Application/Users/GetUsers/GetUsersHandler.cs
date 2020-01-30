using System.Threading;
using System.Threading.Tasks;
using ApiExercise.Application.Common;
using ApiExercise.Application.Common.Queries;
using ApiExercise.Application.Common.ResponseModels;
using MediatR;

namespace ApiExercise.Application.Users.GetUsers
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