using System.Threading;
using System.Threading.Tasks;
using ApiExercise.Application.People.Responses;
using ApiExercise.Application.Shared;

namespace ApiExercise.Application.People.Queries
{
    public interface IGetPeople
    {
        Task<PaginatedResponse<PersonListItemModel>> Query(GetPeopleDataQueryRequest getPeopleDataQuery, CancellationToken cancellationToken);
    }
}
