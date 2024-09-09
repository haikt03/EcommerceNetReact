using Core.Entities;
using System.Linq.Expressions;

namespace Core.Interfaces.IRepositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync
        (
            Expression<Func<T, bool>>? searchExpression = null,
            Expression<Func<T, object>>? sortExpression = null,
            bool sortDescending = false
        );
        Task<T?> GetByIdAsync(int id);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        bool Exists(int id);
    }
}