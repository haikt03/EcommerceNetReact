using Core.Entities;
using Core.Helpers;

namespace Core.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<PagedList<Book>> GetAllAsync(BookParam bookParam);
        Task<PagedList<Book>> GetAllByAuthor(PaginationParam paginationParam ,int authorId);
        Task<PagedList<Book>> GetAllByCategory(PaginationParam paginationParam ,int categoryId);
    }
}
