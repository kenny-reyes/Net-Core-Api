using System.Threading;
using System.Threading.Tasks;
using NetCoreApiScaffolding.Application.Repositories.Users;
using MediatR;

namespace NetCoreApiScaffolding.Application.Users.DeleteUserById
{
    public class DeleteUserByIdHandler : IRequestHandler<DeleteUserByIdRequest>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(DeleteUserByIdRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdWithIncludes(request.Id, cancellationToken);

            _userRepository.Remove(user);

            return Unit.Value;
        }
    }
}