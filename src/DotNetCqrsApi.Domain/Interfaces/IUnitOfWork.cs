using System.Threading.Tasks;

namespace DotNetCqrsApi.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task Save();
    }
}
