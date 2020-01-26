using System.Threading;
using System.Threading.Tasks;

namespace ApiExercise.Application.Users.Queries
{
    public interface IEmailAlreadyExistsCount
    {
        Task<int> Query(string email, int id, CancellationToken cancellationToken);
    }
}
