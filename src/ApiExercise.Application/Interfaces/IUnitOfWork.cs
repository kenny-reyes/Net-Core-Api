using System.Threading.Tasks;

namespace ApiExercise.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task Save();
    }
}
