using Core.Entities;
using Core.Helpers;
using Core.Params;

namespace Core.Interfaces
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<PagedList<Author>> GetAllAsync(PaginationParam paginationParam, string? search = null);
    }
}
