using Core.Entities;
using Core.Helpers;
using Core.Params;

namespace Core.Interfaces
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
        Task<PagedList<Category>> GetAllAsync(PaginationParam paginationParam ,string? search = null);
        Task<PagedList<Category>> GetAllHierarchyAsync(PaginationParam paginationParam);
    }
}
