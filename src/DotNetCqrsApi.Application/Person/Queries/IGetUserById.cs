using System.Threading;
using System.Threading.Tasks;
using DotNetCqrsApi.Application.Person.Responses;

namespace DotNetCqrsApi.Application.Person.Queries
{
    public interface IGetUserById
    {
        Task<PersonModel> Query(int id, CancellationToken cancellationToken);
    }
}
