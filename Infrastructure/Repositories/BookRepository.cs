using Core.Entities;
using Core.Interfaces.IRepositories;
using Infrastructure.Data;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Book>> GetAllAsync(string? search = null, string? categories = null, string? authors = null, string? languages = null, int minPrice = 0, int maxPrice = 0, string? sort = null)
        {
            var query = _context.Books.AsQueryable()
                                        .Search(search)
                                        .Filter(categories, authors, languages, minPrice, maxPrice)
                                        .Sort(sort);

            return await query.ToListAsync();
        }

        public async Task<List<Book>> GetAllByAuthor(int authorId)
        {
            return await _context.Books.Where(b => b.AuthorId == authorId).ToListAsync();
        }

        public async Task<List<Book>> GetAllByCategory(int categoryId)
        {
            return await _context.Books.Where(b => b.CategoryId == categoryId).ToListAsync();
        }
    }
}
