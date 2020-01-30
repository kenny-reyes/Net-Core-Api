using System.Threading;
using System.Threading.Tasks;

namespace ApiExercise.Application.Common.Queries
{
    public interface IIdExists
    {
        Task<bool> Query(int id, CancellationToken cancellationToken);
    }
}
