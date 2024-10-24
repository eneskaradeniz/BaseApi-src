using BaseApi.Domain.Core.Primitives;
using System.Linq.Expressions;

namespace BaseApi.Persistence.Specifications;

//internal abstract class Specification<TEntity>
//    where TEntity : BaseEntity
//{
//    internal abstract Expression<Func<TEntity, bool>> ToExpression();

//    internal bool IsSatisfiedBy(TEntity entity) => ToExpression().Compile()(entity);

//    public static implicit operator Expression<Func<TEntity, bool>>(Specification<TEntity> specification) =>
//        specification.ToExpression();
//}

//internal abstract class Specification<TEntity>
//    where TEntity : BaseEntity
//{
//    internal abstract IQueryable<TEntity> Apply(IQueryable<TEntity> query);

//    internal virtual Expression<Func<TEntity, bool>> ToExpression() => entity => true;

//    internal bool IsSatisfiedBy(TEntity entity) => ToExpression().Compile()(entity);

//    public static implicit operator Expression<Func<TEntity, bool>>(Specification<TEntity> specification) =>
//        specification.ToExpression();
//}

//internal abstract class Specification<TEntity>
//    where TEntity : BaseEntity
//{
//    internal abstract IQueryable<TEntity> Apply(IQueryable<TEntity> query);
//}

internal abstract class Specification<TEntity>
    where TEntity : BaseEntity
{
    internal virtual Expression<Func<TEntity, bool>> ToExpression() => _ => true;

    internal virtual IQueryable<TEntity> Apply(IQueryable<TEntity> query) => query.Where(ToExpression());
}
