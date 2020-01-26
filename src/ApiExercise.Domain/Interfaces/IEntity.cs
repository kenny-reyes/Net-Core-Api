using System.ComponentModel.DataAnnotations;

namespace ApiExercise.Domain.Interfaces
{
    public interface IEntity
    {
        [Key]
        int Id { get; }
    }
}
