using System;
using NetCoreApiScaffolding.Application.Common.ResponseModels;
using MediatR;

namespace NetCoreApiScaffolding.Application.Users.CreateUser
{
    /*
     * NOTE: yes, looks ugly to have 3 classes in the same file, but seems it is like the people do it,
     * well, there is a discussion about this, I don't care to divide it, I don't have an opinion
     */
    public sealed class CreateUserRequest : IRequest<UserResponseModel>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public int GenderId { get; set; }
    }
}
