using System.Threading;
using System.Threading.Tasks;
using NetCoreApiScaffolding.Application.Interfaces;
using NetCoreApiScaffolding.Domain.Users;

namespace NetCoreApiScaffolding.Application.Repositories.Users
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByName(string name, CancellationToken cancellationToken);
        Task<User> FindById(int id, CancellationToken cancellationToken);
        Task<User> FindByIdWithIncludes(int id, CancellationToken cancellationToken);
    }
}
