using System;
using MediatR;

namespace NetCoreApiScaffolding.Application.Users.UpdateUser
{
    /*
     * NOTE: yes, looks ugly to have 3 classes in the same file, but seems it is like the people do it,
     * well, there is a discussion about this, I don't care to divide it, I don't have an opinion
     */
    public sealed class UpdateUserRequest : IRequest
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public int GenderId { get; set; }
    }
}