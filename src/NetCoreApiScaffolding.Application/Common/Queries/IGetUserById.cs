using System.Threading;
using System.Threading.Tasks;
using NetCoreApiScaffolding.Application.Common.ResponseModels;

namespace NetCoreApiScaffolding.Application.Common.Queries
{
    public interface IGetUserById
    {
        Task<UserResponseModel> Query(int id, CancellationToken cancellationToken);
    }
}