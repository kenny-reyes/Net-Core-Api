using MediatR;

namespace NetCoreApiScaffolding.Application.Users.DeleteUserById
{
    public class DeleteUserByIdRequest : IRequest
    {
        public int Id { get; set; }
    }
}