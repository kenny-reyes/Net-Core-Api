using System.Threading;
using System.Threading.Tasks;
using ApiExercise.Application.Repositories.Users;
using MediatR;

namespace ApiExercise.Application.Users.UpdateUser
{
    public sealed class UpdateUserHandler : IRequestHandler<UpdateUserRequest>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<Unit> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdWithIncludes(request.Id, cancellationToken);

            user.Update(
                request.Id,
                request.Email,
                request.Name,
                request.Birthdate,
                request.GenderId);

            return Unit.Value;
        }
    }
}