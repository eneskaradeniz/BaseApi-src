using BaseApi.Domain.Core.Primitives;
using BaseApi.Domain.Core.Primitives.Maybe;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BaseApi.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<TEntity> Set<TEntity>()
        where TEntity : BaseEntity;

    Task<Maybe<TEntity>> GetByIdAsync<TEntity, TId, TValue>(TId id, CancellationToken cancellationToken = default)
        where TEntity : Entity<TId, TValue>
        where TId : StronglyTypedId<TValue>;

    Task<bool> AnyAsync<TEntity, TId, TValue>(TId id, CancellationToken cancellationToken)
        where TEntity : Entity<TId, TValue>
        where TId : StronglyTypedId<TValue>;

    void Insert<TEntity>(TEntity entity)
        where TEntity : BaseEntity;

    void InsertRange<TEntity>(IReadOnlyCollection<TEntity> entities)
        where TEntity : BaseEntity;

    void Update<TEntity>(TEntity entity)
        where TEntity : BaseEntity;

    void Remove<TEntity>(TEntity entity)
        where TEntity : BaseEntity;

    void RemoveRange<TEntity>(IReadOnlyCollection<TEntity> entities)
        where TEntity : BaseEntity;

    Task<int> CountAsync<TEntity>(CancellationToken cancellationToken = default)
        where TEntity : BaseEntity;

    Task<int> ExecuteSqlAsync(string sql, IEnumerable<SqlParameter> parameters, CancellationToken cancellationToken = default);
}