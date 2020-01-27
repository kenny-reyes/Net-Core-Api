using System.Threading;
using System.Threading.Tasks;
using ApiExercise.Application.Repositories.Users;
using MediatR;

namespace ApiExercise.Application.Users
{
    public class DeleteUserByIdRequest : IRequest
    {
        public int Id { get; set; }
    }
    
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

            // TODO: Add entity validation to checking existing indices
             if (user == null) throw new System.Exception($"The requested identifier {request.Id} to Delete doesn't exists");
             
            _userRepository.Remove(user);

            return Unit.Value;
        }
    }
}
