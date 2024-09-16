using Core.Entities;

namespace Core.Interfaces.IRepositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<List<Book>> GetAllAsync
        (
            string? search = null,
            string? categories = null,
            string? authors = null,
            string? languages = null,
            int minPrice = 0,
            int maxPrice = 0,
            string? sort = null
        );
        Task<List<Book>> GetAllByAuthor(int authorId);
        Task<List<Book>> GetAllByCategory(int categoryId);
    }
}
