using System.Threading.Tasks;

namespace NetCoreApiScaffolding.Tools.Contracts
{
    public interface IUnitOfWork
    {
        Task Save();
    }
}