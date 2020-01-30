using System.Threading;
using System.Threading.Tasks;
using ApiExercise.Application.Common.ResponseModels;

namespace ApiExercise.Application.Common.Queries
{
    public interface IGetUserById
    {
        Task<UserResponseModel> Query(int id, CancellationToken cancellationToken);
    }
}
