using BaseApi.Application.Abstractions.Data;
using BaseApi.Domain.Core.Primitives;
using BaseApi.Domain.Core.Primitives.Maybe;
using BaseApi.Persistence.Specifications;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Persistence.Repositories
{
    internal abstract class GenericRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected GenericRepository(IApplicationDbContext dbContext) => DbContext = dbContext;

        protected IApplicationDbContext DbContext { get; }

        public virtual void Insert(TEntity entity) 
            => DbContext.Insert(entity);

        public virtual void InsertRange(IReadOnlyCollection<TEntity> entities) 
            => DbContext.InsertRange(entities);

        public virtual void Update(TEntity entity) 
            => DbContext.Update(entity);

        public virtual void Remove(TEntity entity) 
            => DbContext.Remove(entity);

        public virtual void RemoveRange(IReadOnlyCollection<TEntity> entities) 
            => DbContext.RemoveRange(entities);

        protected virtual async Task<int> CountAsync(CancellationToken cancellationToken = default) 
            => await DbContext.CountAsync<TEntity>(cancellationToken);

        protected async Task<bool> AnyAsync(Specification<TEntity> specification,
            CancellationToken cancellationToken = default)
        {
            var query = specification.Apply(DbContext.Set<TEntity>());

            return await query.AnyAsync(specification.ToExpression(), cancellationToken);
        }

        protected async Task<Maybe<TEntity>> FirstOrDefaultAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default)
        {
            var query = specification.Apply(DbContext.Set<TEntity>());

            return await query.FirstOrDefaultAsync(specification.ToExpression(), cancellationToken);
        }
    }

    internal abstract class GenericRepository<TEntity, TId, TValue> : GenericRepository<TEntity>
        where TEntity : Entity<TId, TValue>
        where TId : StronglyTypedId<TValue>
    {
        protected GenericRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AnyAsync(TId id, CancellationToken cancellationToken = default)
            => await DbContext.AnyAsync<TEntity, TId, TValue>(id, cancellationToken);

        public async Task<Maybe<TEntity>> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
            => await DbContext.GetByIdAsync<TEntity, TId, TValue>(id, cancellationToken);
    }
}
