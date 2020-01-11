using System.Threading;
using System.Threading.Tasks;

namespace DotNetCqrsApi.Application.Person.Queries
{
    public interface IEmailAlreadyExistsCount
    {
        Task<int> Query(string email, int id, CancellationToken cancellationToken);
    }
}
