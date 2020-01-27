using System.Threading;
using System.Threading.Tasks;
using ApiExercise.Application.Users.ResponseModels;

namespace ApiExercise.Application.Users.Queries
{
    public interface IGetUserById
    {
        Task<UserResponseModel> Query(int id, CancellationToken cancellationToken);
    }
}
