using System.Threading;
using System.Threading.Tasks;
using DotNetCqrsApi.Application.People.Responses;

namespace DotNetCqrsApi.Application.People.Queries
{
    public interface IGetPersonById
    {
        Task<PersonModel> Query(int id, CancellationToken cancellationToken);
    }
}
