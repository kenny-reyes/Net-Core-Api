using System.Threading;
using System.Threading.Tasks;

namespace NetCoreApiScaffolding.Application.Common.Queries
{
    public interface IEmailAlreadyExistsCount
    {
        Task<int> Query(string email, int id, CancellationToken cancellationToken);
    }
}
