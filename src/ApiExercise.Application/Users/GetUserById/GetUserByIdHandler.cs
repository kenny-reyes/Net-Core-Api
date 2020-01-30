using System.Threading;
using System.Threading.Tasks;
using ApiExercise.Application.Common.Queries;
using ApiExercise.Application.Common.ResponseModels;
using MediatR;

namespace ApiExercise.Application.Users.GetUserById
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