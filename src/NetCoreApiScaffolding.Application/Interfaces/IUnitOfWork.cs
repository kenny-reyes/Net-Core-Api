using System.Threading.Tasks;

namespace NetCoreApiScaffolding.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task Save();
    }
}
