using Core.Entities;
using Core.Interfaces.IRepositories;
using Infrastructure.Data;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Category>> GetAllAsync(string? search = null)
        {
            var query = _context.Categories.AsQueryable().Search(search).OrderBy(c => c.Name);

            return await query.ToListAsync();
        }

        public async Task<List<Category>> GetAllHierarchyAsync()
        {
            var query = _context.Categories.AsQueryable().GetHierarchicalCategories();

            return await query.ToListAsync();
        }
    }
}
