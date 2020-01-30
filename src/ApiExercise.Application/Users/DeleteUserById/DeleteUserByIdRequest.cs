using MediatR;

namespace ApiExercise.Application.Users.DeleteUserById
{
    public class DeleteUserByIdRequest : IRequest
    {
        public int Id { get; set; }
    }
}