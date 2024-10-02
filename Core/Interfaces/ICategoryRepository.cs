using Core.Entities;
using Core.Helpers;
using Core.Utils;

namespace Core.Interfaces
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
        Task<PagedList<Category>> GetAllAsync(PaginationParam paginationParam ,string? search = null);
    }
}
