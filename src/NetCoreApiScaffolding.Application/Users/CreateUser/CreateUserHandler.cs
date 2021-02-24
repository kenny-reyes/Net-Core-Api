using System.Threading;
using System.Threading.Tasks;
using NetCoreApiScaffolding.Application.Common.ResponseModels;
using NetCoreApiScaffolding.Application.Repositories.Users;
using NetCoreApiScaffolding.Domain.Users;
using MediatR;

namespace NetCoreApiScaffolding.Application.Users.CreateUser
{
    public sealed class CreateUserHandler : IRequestHandler<CreateUserRequest, UserResponseModel>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<UserResponseModel> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = User.Create(
                request.Email,
                request.Name,
                request.Birthdate,
                request.GenderId);

            await _userRepository.Add(user, cancellationToken);
            return new UserResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Birthdate = user.Birthdate,
                GenderId = user.GenderId
            };
        }
    }
}