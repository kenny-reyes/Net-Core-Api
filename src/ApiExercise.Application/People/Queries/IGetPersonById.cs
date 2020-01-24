using System.Threading;
using System.Threading.Tasks;
using ApiExercise.Application.People.Responses;

namespace ApiExercise.Application.People.Queries
{
    public interface IGetPersonById
    {
        Task<PersonModel> Query(int id, CancellationToken cancellationToken);
    }
}
