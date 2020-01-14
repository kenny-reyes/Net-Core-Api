using System.Threading;
using System.Threading.Tasks;
using DotNetCqrsApi.Application.Person.Responses;
using DotNetCqrsApi.Application.Shared;

namespace DotNetCqrsApi.Application.Person.Queries
{
    public interface IGetPeople
    {
        Task<PaginatedResponse<PersonListItemModel>> Query(GetPeopleDataQueryRequest getPeopleDataQuery, CancellationToken cancellationToken);
    }
}
