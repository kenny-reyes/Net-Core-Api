using System.Threading;
using System.Threading.Tasks;

namespace NetCoreApiScaffolding.Application.Interfaces.Queries
{
    public interface IIdExists
    {
        Task<bool> Query(int id, CancellationToken cancellationToken);
    }
}