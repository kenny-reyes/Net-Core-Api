using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ApiExercise.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<TEntity> Find(int id, CancellationToken cancellationToken);

        Task<TEntity> Find(int id, CancellationToken cancellationToken,
            params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken);

        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includes);

        Task Add(TEntity entity, CancellationToken cancellationToken);
        void Remove(TEntity entity);
    }
}