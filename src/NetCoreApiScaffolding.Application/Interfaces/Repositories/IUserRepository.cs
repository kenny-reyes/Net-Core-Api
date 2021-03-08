using System.Threading;
using System.Threading.Tasks;
using NetCoreApiScaffolding.Domain.Users;

namespace NetCoreApiScaffolding.Application.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByName(string name, CancellationToken cancellationToken);
        Task<User> FindById(int id, CancellationToken cancellationToken);
        Task<User> FindByIdWithIncludes(int id, CancellationToken cancellationToken);
    }
}