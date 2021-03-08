using System.Threading;
using System.Threading.Tasks;
using NetCoreApiScaffolding.Domain.Users;
using NetCoreApiScaffolding.Infrastructure.Common;
using NetCoreApiScaffolding.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using NetCoreApiScaffolding.Application.Interfaces.Repositories;

namespace NetCoreApiScaffolding.Infrastructure.Repositories.Users
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataBaseContext context)
            : base(context)
        {
        }

        public async Task<User> FindByName(string name, CancellationToken cancellationToken)
        {
            return await Context.Set<User>()
                .SingleOrDefaultAsync(u => u.Name == name, cancellationToken);
        }

        public async Task<User> FindById(int id, CancellationToken cancellationToken)
        {
            return await Context.Set<User>()
                .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task<User> FindByIdWithIncludes(int id, CancellationToken cancellationToken)
        {
            return await Context.Set<User>()
                .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
        }
    }
}