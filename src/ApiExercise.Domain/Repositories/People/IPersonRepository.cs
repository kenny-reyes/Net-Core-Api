using System.Threading;
using System.Threading.Tasks;
using ApiExercise.Domain.Interfaces;
using ApiExercise.Domain.People;

namespace ApiExercise.Domain.Repositories.People
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<Person> FindByName(string name, CancellationToken cancellationToken);
        Task<Person> FindById(int id, CancellationToken cancellationToken);
        Task<Person> FindByIdWithIncludes(int id, CancellationToken cancellationToken);
    }
}
