using System.Threading;
using System.Threading.Tasks;

namespace DotNetCqrsApi.Application.People.Queries
{
    public interface IEmailAlreadyExistsCount
    {
        Task<int> Query(string email, int id, CancellationToken cancellationToken);
    }
}
