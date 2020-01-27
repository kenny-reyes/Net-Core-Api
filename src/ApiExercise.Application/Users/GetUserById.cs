using System.Threading;
using System.Threading.Tasks;
using ApiExercise.Application.Users.Queries;
using ApiExercise.Application.Shared;
using ApiExercise.Application.Users.ResponseModels;
using MediatR;

namespace ApiExercise.Application.Users
{
    public class GetUserByIdRequest : IRequest<UserResponseModel>
    {
        public int Id { get; set; }
    }
    
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
