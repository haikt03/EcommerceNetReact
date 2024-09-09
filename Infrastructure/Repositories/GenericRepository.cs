using Core.Entities;
using Core.Interfaces.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>>? searchExpression = null, Expression<Func<T, object>>? sortExpression = null, bool sortDescending = false)
        {
            var query = _context.Set<T>().AsQueryable();

            if (searchExpression != null)
            {
                query = query.Where(searchExpression);
            }

            if (sortExpression != null)
            {
                query = sortDescending
                    ? query.OrderByDescending(sortExpression)
                    : query.OrderBy(sortExpression);
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public bool Exists(int id)
        {
            return _context.Set<T>().Any(x => x.Id == id);
        }
    }
}
