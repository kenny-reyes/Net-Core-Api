using System.Threading.Tasks;

namespace ApiExercise.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task Save();
    }
}
