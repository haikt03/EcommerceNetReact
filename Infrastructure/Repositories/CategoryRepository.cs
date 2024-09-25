using Core.Entities;
using Core.Helpers;
using Core.Interfaces;
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

        public async Task<PagedList<Category>> GetAllAsync(PaginationParam paginationParam, string? search = null)
        {
            var query = _context.Categories.AsQueryable().Search(search).OrderBy(c => c.Name);
            var result = await query.ToPagedListAsync(paginationParam.PageSize, paginationParam.PageIndex);

            return result;
        }

        public async Task<List<Category>> GetAllHierarchyAsync(int? parentId, int? currentDepth, int? maxDepth)
        {
            var result = await _context.Categories.AsQueryable().GetHierarchicalCategories(parentId, currentDepth, maxDepth).ToListAsync();
            return result;
        }
    }
}
