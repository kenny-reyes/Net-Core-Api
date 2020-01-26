using System.Threading;
using System.Threading.Tasks;
using ApiExercise.Application.Interfaces;
using ApiExercise.Domain.Users;

namespace ApiExercise.Application.Repositories.Users
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByName(string name, CancellationToken cancellationToken);
        Task<User> FindById(int id, CancellationToken cancellationToken);
        Task<User> FindByIdWithIncludes(int id, CancellationToken cancellationToken);
    }
}
