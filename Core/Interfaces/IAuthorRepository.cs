using Core.Entities;
using Core.Helpers;
using Core.Utils;

namespace Core.Interfaces
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<PagedList<Author>> GetAllAsync(PaginationParam paginationParam, string? search = null);
    }
}
