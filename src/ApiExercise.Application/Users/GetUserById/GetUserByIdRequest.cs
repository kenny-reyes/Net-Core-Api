using ApiExercise.Application.Common.ResponseModels;
using MediatR;

namespace ApiExercise.Application.Users.GetUserById
{
    public class GetUserByIdRequest : IRequest<UserResponseModel>
    {
        public int Id { get; set; }
    }
}
