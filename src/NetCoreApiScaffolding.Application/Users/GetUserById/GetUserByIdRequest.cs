using NetCoreApiScaffolding.Application.Common.ResponseModels;
using MediatR;

namespace NetCoreApiScaffolding.Application.Users.GetUserById
{
    public class GetUserByIdRequest : IRequest<UserResponseModel>
    {
        public int Id { get; set; }
    }
}
