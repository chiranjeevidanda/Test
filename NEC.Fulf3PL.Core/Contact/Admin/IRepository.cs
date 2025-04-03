using NEC.Fulf3PL.Core.Entities.Persistence;

namespace NEC.Fulf3PL.Core.Contact.Admin;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> CreateAsync(TEntity entity);

    Task<TEntity> UpdateAsync(TEntity model);

    Task DeleteAsync(TEntity entity);

    Task DeleteAsync(string id);

    Task<TEntity> GetAsync(string id);
}
