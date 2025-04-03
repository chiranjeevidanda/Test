using NEC.Fulf3PL.Core.Entities;
using System.Linq.Expressions;

namespace NEC.Fulf3PL.Core
{
    /// <summary>
    /// Interface for generic repository methods
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// 
    public interface ICosmosRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAsync();

        public Task<IEnumerable<T>> GetByQueryAsync(string inputQuery);

        public Task<T> GetAsync(string id);

        public Task<T> CreateAsync(T entity);

        public Task<T> UpdateAsync(string id, T entity);

        public Task DeleteAsync(string id);
    }
}
