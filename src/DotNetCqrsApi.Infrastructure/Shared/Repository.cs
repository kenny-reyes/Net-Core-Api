﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DotNetCqrsApi.Domain.Interfaces;
using DotNetCqrsApi.Infrastructure.Context;
using DotNetCqrsApi.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DotNetCqrsApi.Infrastructure.Shared
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly MyContext Context;

        public Repository(MyContext context)
        {
            Context = context;
        }

        public virtual async Task<TEntity> Find(int id, CancellationToken cancellationToken)
        {
            return await Context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
        {
            return await Context.Set<TEntity>().Where(filter).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includes)
        {
            return await Context.Set<TEntity>().Where(filter).IncludeMultiple(includes).ToListAsync(cancellationToken);
        }

        public async Task<TEntity> Find(int id, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includes)
        {
            return await Context.Set<TEntity>().IncludeMultiple(includes).SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task Add(TEntity entity, CancellationToken cancellationToken)
        {
            await Context.AddAsync(entity, cancellationToken);
        }

        public void Remove(TEntity entity)
        {
            Context.Remove(entity);
        }
    }
}