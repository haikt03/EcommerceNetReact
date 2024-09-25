using Core.Entities;
using Core.Helpers;

namespace Core.Interfaces
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
        Task<PagedList<Category>> GetAllAsync(PaginationParam paginationParam ,string? search = null);
        Task<List<Category>> GetAllHierarchyAsync(int? parentId, int? currentDepth, int? maxDepth);
    }
}
