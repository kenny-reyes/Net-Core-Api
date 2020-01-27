using System.Threading;
using System.Threading.Tasks;
using ApiExercise.Application.Users.Queries;
using ApiExercise.Application.Shared;
using ApiExercise.Application.Users.ResponseModels;
using MediatR;

namespace ApiExercise.Application.Users
{
    public class GetUsersRequest : PaginatedRequest, IRequest<PaginatedResponse<UserListItemResponseModel>>
    { }
    
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
