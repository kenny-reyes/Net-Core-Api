using System.Threading;
using System.Threading.Tasks;
using ApiExercise.Application.Repositories.Users;
using ApiExercise.Domain.Users;
using ApiExercise.Infrastructure.Context;
using ApiExercise.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace ApiExercise.Infrastructure.Repositories.People
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ExerciseContext context)
           : base(context)
        { }

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

