using MediatR;
using NetCoreApiScaffolding.Application.ResponseModels;

namespace NetCoreApiScaffolding.Application.Users.GetUserById
{
    public class GetUserByIdRequest : IRequest<UserResponseModel>
    {
        public int Id { get; set; }
    }
}