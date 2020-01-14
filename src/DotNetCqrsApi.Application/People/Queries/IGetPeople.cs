using System.Threading;
using System.Threading.Tasks;
using DotNetCqrsApi.Application.People.Responses;
using DotNetCqrsApi.Application.Shared;

namespace DotNetCqrsApi.Application.People.Queries
{
    public interface IGetPeople
    {
        Task<PaginatedResponse<PersonListItemModel>> Query(GetPeopleDataQueryRequest getPeopleDataQuery, CancellationToken cancellationToken);
    }
}
