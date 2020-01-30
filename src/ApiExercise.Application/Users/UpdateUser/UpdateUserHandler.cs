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

            // TODO: Add validation to checking existing indices (This line is not at all definitive, only for test purposes)
            if (user == null) throw new System.Exception($"The requested identifier {request.Id} to Update doesn't exists");

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