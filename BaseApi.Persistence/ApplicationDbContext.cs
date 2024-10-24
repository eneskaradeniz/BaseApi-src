using BaseApi.Application.Abstractions.Common;
using BaseApi.Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using MediatR;
using BaseApi.Domain.Core.Abstractions;
using BaseApi.Domain.Core.Events;
using BaseApi.Domain.Core.Primitives;
using BaseApi.Persistence.Extensions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;
using BaseApi.Domain.Core.Primitives.Maybe;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;

namespace BaseApi.Persistence;

public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IDateTime dateTime,
    IMediator mediator,
    IApplicationSeedData baseApiSeedData)
    : DbContext(options), IApplicationDbContext, IUnitOfWork
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.ApplyUtcDateTimeConverter();

        baseApiSeedData.Run(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private void UpdateAuditableEntities(DateTime utcNow)
    {
        foreach (EntityEntry<IAuditableEntity> entityEntry in ChangeTracker.Entries<IAuditableEntity>())
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(nameof(IAuditableEntity.CreatedOnUtc)).CurrentValue = utcNow;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(nameof(IAuditableEntity.ModifiedOnUtc)).CurrentValue = utcNow;
            }
        }
    }

    private void UpdateSoftDeletableEntities(DateTime utcNow)
    {
        foreach (EntityEntry<ISoftDeletableEntity> entityEntry in ChangeTracker.Entries<ISoftDeletableEntity>())
        {
            if (entityEntry.State != EntityState.Deleted)
            {
                continue;
            }

            entityEntry.Property(nameof(ISoftDeletableEntity.DeletedOnUtc)).CurrentValue = utcNow;

            entityEntry.Property(nameof(ISoftDeletableEntity.Deleted)).CurrentValue = true;

            entityEntry.State = EntityState.Modified;

            UpdateDeletedEntityEntryReferencesToUnchanged(entityEntry);
        }
    }

    private static void UpdateDeletedEntityEntryReferencesToUnchanged(EntityEntry entityEntry)
    {
        if (!entityEntry.References.Any())
        {
            return;
        }

        foreach (ReferenceEntry referenceEntry in entityEntry.References.Where(r => r.TargetEntry!.State == EntityState.Deleted))
        {
            referenceEntry.TargetEntry!.State = EntityState.Unchanged;

            UpdateDeletedEntityEntryReferencesToUnchanged(referenceEntry.TargetEntry);
        }
    }

    private async Task PublishDomainEvents(CancellationToken cancellationToken)
    {
        var aggregateRoots = ChangeTracker
            .Entries()
            .Where(e => e.Entity is AggregateRoot<StronglyTypedId<object>, object>)
            .Cast<AggregateRoot<StronglyTypedId<object>, object>>()
            .ToList();

        List<IDomainEvent> domainEvents = aggregateRoots
            .SelectMany(ar => ar.DomainEvents)
            .ToList();

        aggregateRoots.ForEach(ar => ar.ClearDomainEvents());

        var tasks = domainEvents.Select(domainEvent => mediator.Publish(domainEvent, cancellationToken));

        await Task.WhenAll(tasks);
    }

    #region IUnitOfWork

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        DateTime utcNow = dateTime.UtcNow;

        UpdateAuditableEntities(utcNow);

        UpdateSoftDeletableEntities(utcNow);

        await PublishDomainEvents(cancellationToken);

        return await base.SaveChangesAsync(cancellationToken);
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        => Database.BeginTransactionAsync(cancellationToken);

    #endregion

    #region IDbContext

    public new DbSet<TEntity> Set<TEntity>()
        where TEntity : BaseEntity
        => base.Set<TEntity>();

    public async Task<Maybe<TEntity>> GetByIdAsync<TEntity, TId, TValue>(TId id, CancellationToken cancellationToken = default)
        where TEntity : Entity<TId, TValue>
        where TId : StronglyTypedId<TValue>
        => id == null || id.Equals(default(TId)) ?
            Maybe<TEntity>.None :
            Maybe<TEntity>.From(await Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken));

    public async Task<bool> AnyAsync<TEntity, TId, TValue>(TId id, CancellationToken cancellationToken)
        where TEntity : Entity<TId, TValue>
        where TId : StronglyTypedId<TValue>
        => await Set<TEntity>().AnyAsync(e => e.Id == id, cancellationToken);

    public void Insert<TEntity>(TEntity entity)
        where TEntity : BaseEntity
        => Set<TEntity>().Add(entity);

    public void InsertRange<TEntity>(IReadOnlyCollection<TEntity> entities)
        where TEntity : BaseEntity
        => Set<TEntity>().AddRange(entities);

    public new void Update<TEntity>(TEntity entity)
        where TEntity : BaseEntity
        => Set<TEntity>().Update(entity);

    public new void Remove<TEntity>(TEntity entity)
        where TEntity : BaseEntity
        => Set<TEntity>().Remove(entity);

    public void RemoveRange<TEntity>(IReadOnlyCollection<TEntity> entities)
        where TEntity : BaseEntity
        => Set<TEntity>().RemoveRange(entities);

    public async Task<int> CountAsync<TEntity>(CancellationToken cancellationToken = default)
        where TEntity : BaseEntity
        => await Set<TEntity>().CountAsync(cancellationToken);

    public async Task<int> ExecuteSqlAsync(string sql, IEnumerable<SqlParameter> parameters, CancellationToken cancellationToken = default)
        => await Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);

    #endregion
}
