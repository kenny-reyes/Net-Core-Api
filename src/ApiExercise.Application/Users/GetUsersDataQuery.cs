using System.Threading;
using System.Threading.Tasks;
using ApiExercise.Application.Users.Queries;
using ApiExercise.Application.Shared;
using ApiExercise.Application.Users.ResponseModels;
using MediatR;

namespace ApiExercise.Application.Users
{
    public class GetUsersDataQueryRequest : PaginatedRequest, IRequest<PaginatedResponse<UserListItemModel>>
    { }
    
    public class GetUsersDataQueryHandler : IRequestHandler<GetUsersDataQueryRequest, PaginatedResponse<UserListItemModel>>
    {                                                                         
        private readonly IGetUsers _getUsersQueries;

        public GetUsersDataQueryHandler(IGetUsers getUsersQueries)
        {
            _getUsersQueries = getUsersQueries;
        }
        
        public async Task<PaginatedResponse<UserListItemModel>> Handle(GetUsersDataQueryRequest request, CancellationToken cancellationToken)
        {
            return await _getUsersQueries.Query(request, cancellationToken);
        }
    }
}
