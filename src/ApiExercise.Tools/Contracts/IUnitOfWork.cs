using System.Threading.Tasks;

namespace ApiExercise.Tools.Contracts
{
    public interface IUnitOfWork
    {
        Task Save();
    }
}
