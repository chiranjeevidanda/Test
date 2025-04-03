using NEC.Fulf3PL.Core.Entities.Persistence;
using System.Linq.Expressions;

namespace NEC.Fulf3PL.Application.Common;

public interface IQueryService<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetItemsAsync(Expression<Func<TEntity, bool>> predicate);

    Task<IEnumerable<TResult>> GetItemsAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> projection);

    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

    Task<TResult?> GetItemAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> projection);

    Task<TEntity> GetItemAsync(string id);

    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

    Task<int> CountAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> projection);
}
