using System.ComponentModel.DataAnnotations;

namespace NetCoreApiScaffolding.Domain.Interfaces
{
    public interface IEntity
    {
        [Key]
        int Id { get; }
    }
}
