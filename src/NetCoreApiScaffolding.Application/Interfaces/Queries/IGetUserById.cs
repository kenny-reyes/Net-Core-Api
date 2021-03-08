using System.Threading;
using System.Threading.Tasks;
using NetCoreApiScaffolding.Application.ResponseModels;

namespace NetCoreApiScaffolding.Application.Interfaces.Queries
{
    public interface IGetUserById
    {
        Task<UserResponseModel> Query(int id, CancellationToken cancellationToken);
    }
}